using NAudio.Midi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LavaManager.Converters
{
    class RBN2toRBN1
    {
        Stream midiStream;
        string output;

        public RBN2toRBN1(Stream midiFile, string outPath)
        {
            output = outPath;
            midiStream = midiFile;
        }

        /* FixMIDI function taken from C3 CON Tools open source release
         * https://github.com/RBTools/CON-Tools/blob/master/C3%20Tools/WiiConverter.cs
         * Thanks, TrojanNemo! */
        public bool FixMIDI()
        {
            var rbn2 = new MidiFile(midiStream, false);
            if (rbn2 == null)
            {
                return false;
            }
            var note_length = rbn2.DeltaTicksPerQuarterNote / 4; //16th note
            var to_remove = new List<MidiEvent>();
            var to_add = new List<MidiEvent>();
            var venue = -1;
            long lastevent = (note_length * -1) - 1; //this ensures (lastevent + note_length) starts at -1
            long final_event = 0;

            for (var i = 0; i < rbn2.Events.Tracks; i++)
            {
                if (!rbn2.Events[i][0].ToString().Contains("VENUE")) continue;
                venue = i;
                long last_first = 0;
                long last_next = 0;
                long last_proc_time = 0;
                var last_proc_note = 0;

                to_add.Add(new TextEvent("[verse]", MetaEventType.TextEvent, 0));
                foreach (var venue_events in rbn2.Events[i])
                {
                    final_event = venue_events.AbsoluteTime;
                    if (venue_events.CommandCode == MidiCommandCode.MetaEvent && venue_events.ToString().Contains("["))
                    {
                        var venue_event = (MetaEvent)venue_events;
                        var index = venue_event.ToString().IndexOf("[", StringComparison.Ordinal);
                        var new_event = venue_event.ToString().Substring(index, venue_event.ToString().Length - index).Trim();

                        if (new_event.Contains("[directed"))
                        {
                            new_event = new_event.Replace("[directed_vocals_cam_pt]", "[directed_vocals_cam]");
                            new_event = new_event.Replace("[directed_vocals_cam_pr]", "[directed_vocals_cam]");
                            new_event = new_event.Replace("[directed_guitar_cam_pt]", "[directed_guitar_cam]");
                            new_event = new_event.Replace("[directed_guitar_cam_pr]", "[directed_guitar_cam]");
                            new_event = new_event.Replace("[directed_crowd]", "[directed_crowd_g]");
                            new_event = new_event.Replace("[directed_duo_drums]", "[directed_drums]");

                            new_event = new_event.Replace("[directed_duo_kv]", "[directed_duo_guitar]");
                            //keys not supported
                            new_event = new_event.Replace("[directed_duo_kb]", "[directed_duo_gb]");
                            new_event = new_event.Replace("[directed_duo_kg]", "[directed_duo_gb]");
                            //all instances replaced
                            new_event = new_event.Replace("[directed_keys]", "[directed_crowd_b]");
                            new_event = new_event.Replace("[directed_keys_cam]", "[directed_crowd_b]");
                            //with arbitrary choices
                            new_event = new_event.Replace("[directed_keys_np]", "[directed_crowd_b]");

                            new_event = new_event.Replace("[directed", "[do_directed_cut directed");
                            to_add.Add(new TextEvent(new_event, MetaEventType.TextEvent, venue_events.AbsoluteTime));
                        }
                        else if (new_event.Contains("[lighting") && venue_events.AbsoluteTime == 0)
                        {
                            new_event = "[lighting ()]";
                            to_add.Add(new TextEvent(new_event, MetaEventType.TextEvent, venue_events.AbsoluteTime));
                        }
                        else if (new_event.Contains("[lighting (manual") || new_event.Contains("[lighting (dischord)]"))
                        {
                            if (venue_events.AbsoluteTime <= last_next)
                            {
                                to_remove.Add(venue_events);
                                continue;
                            }

                            //add First Frame note as found in most RBN1 MIDIs
                            var note = new NoteOnEvent(venue_events.AbsoluteTime, 1, 50, 96, note_length);
                            to_add.Add(note);
                            to_add.Add(new NoteEvent(note.AbsoluteTime + note.NoteLength, note.Channel, MidiCommandCode.NoteOff, note.NoteNumber, 0));
                            last_first = note.AbsoluteTime + note.NoteLength; //to prevent having both Next and First events in the same spot
                            continue;
                        }
                        else if (new_event.Contains("[lighting (verse)]"))
                        {
                            new_event = "[verse]";
                            to_add.Add(new TextEvent(new_event, MetaEventType.TextEvent, venue_events.AbsoluteTime));
                        }
                        else if (new_event.Contains("[lighting (chorus)]"))
                        {
                            new_event = "[chorus]";
                            to_add.Add(new TextEvent(new_event, MetaEventType.TextEvent, venue_events.AbsoluteTime));
                        }
                        else if (new_event.Contains("[lighting (intro)]"))
                        {
                            new_event = "[lighting ()]";
                            to_add.Add(new TextEvent(new_event, MetaEventType.TextEvent, venue_events.AbsoluteTime));
                        }
                        else if (new_event.Contains("[lighting (blackout_spot)]"))
                        {
                            new_event = "[lighting (silhouettes_spot)]";
                            to_add.Add(new TextEvent(new_event, MetaEventType.TextEvent, venue_events.AbsoluteTime));
                        }
                        else if (new_event.Contains("[next]"))
                        {
                            if (venue_events.AbsoluteTime <= last_first)
                            {
                                to_remove.Add(venue_events);
                                continue;
                            }

                            var note = new NoteOnEvent(venue_events.AbsoluteTime, 1, 48, 96, note_length);
                            to_add.Add(note);
                            to_add.Add(new NoteEvent(note.AbsoluteTime + note.NoteLength, note.Channel, MidiCommandCode.NoteOff, note.NoteNumber, 0));
                            last_next = note.AbsoluteTime + note.NoteLength; //to prevent having both Next and First events in the same spot
                        }
                        else if (new_event.Contains(".pp]"))
                        {
                            var note = new NoteOnEvent(venue_events.AbsoluteTime, 1, 0, 96, note_length);
                            switch (new_event)
                            {
                                case "[ProFilm_a.pp]":
                                case "[ProFilm_b.pp]":
                                    note.NoteNumber = 96;
                                    break;
                                case "[film_contrast.pp]":
                                case "[film_contrast_green.pp]":
                                case "[film_contrast_red.pp]":
                                case "[contrast_a.pp]":
                                    note.NoteNumber = 97;
                                    break;
                                case "[desat_posterize_trails.pp]":
                                case "[film_16mm.pp]":
                                    note.NoteNumber = 98;
                                    break;
                                case "[film_sepia_ink.pp]":
                                    note.NoteNumber = 99;
                                    break;
                                case "[film_silvertone.pp]":
                                    note.NoteNumber = 100;
                                    break;
                                case "[horror_movie_special.pp]":
                                case "[ProFilm_psychedelic_blue_red.pp]":
                                case "[photo_negative.pp]":
                                    note.NoteNumber = 101;
                                    break;
                                case "[photocopy.pp]":
                                    note.NoteNumber = 102;
                                    break;
                                case "[posterize.pp]":
                                case "[bloom.pp]":
                                    note.NoteNumber = 103;
                                    break;
                                case "[bright.pp]":
                                    note.NoteNumber = 104;
                                    break;
                                case "[ProFilm_mirror_a.pp]":
                                    note.NoteNumber = 105;
                                    break;
                                case "[desat_blue.pp]":
                                case "[film_contrast_blue.pp]":
                                case "[film_blue_filter.pp]":
                                    note.NoteNumber = 106;
                                    break;
                                case "[video_a.pp]":
                                    note.NoteNumber = 107;
                                    break;
                                case "[video_bw.pp]":
                                case "[film_b+w.pp]":
                                    note.NoteNumber = 108;
                                    break;
                                case "[shitty_tv.pp]":
                                case "[video_security.pp]":
                                    note.NoteNumber = 109;
                                    break;
                                case "[video_trails.pp]":
                                case "[flicker_trails.pp]":
                                case "[space_woosh.pp]":
                                case "[clean_trails.pp]":
                                    note.NoteNumber = 110;
                                    break;
                            }

                            //reduces instances of pp notes to bare minimum
                            if (note.NoteNumber > 0 && note.NoteNumber != last_proc_note && note.AbsoluteTime >= last_proc_time)
                            {
                                to_add.Add(note);
                                to_add.Add(new NoteEvent(note.AbsoluteTime + note.NoteLength, note.Channel, MidiCommandCode.NoteOff, note.NoteNumber, 0));
                            }

                            //we want at least 1 measure between pp effects
                            last_proc_time = note.AbsoluteTime + (rbn2.DeltaTicksPerQuarterNote * 4);
                            //we don't want to put multiple PP notes for the same effect
                            last_proc_note = note.NoteNumber;
                        }
                        else if (new_event.Contains("[coop"))
                        {
                            if (venue_events.AbsoluteTime <= (lastevent + note_length)) //to avoid double notes)
                            {
                                to_remove.Add(venue_events);
                                continue;
                            }

                            var cameranotes = new NoteOnEvent[9];
                            var enabled = new bool[9];
                            lastevent = venue_events.AbsoluteTime;

                            const int cameracut = 0; //60
                            const int bass = 1; //61
                            const int drummer = 2; //62
                            const int guitar = 3; //63
                            const int vocals = 4; //64
                            const int nobehind = 5; //70
                            const int onlyfar = 6; //71
                            const int onlyclose = 7; //72
                            const int noclose = 8; //73

                            cameranotes[cameracut] = new NoteOnEvent(venue_events.AbsoluteTime, 1, 60, 96, note_length);
                            cameranotes[bass] = new NoteOnEvent(venue_events.AbsoluteTime, 1, 61, 96, note_length);
                            cameranotes[drummer] = new NoteOnEvent(venue_events.AbsoluteTime, 1, 62, 96, note_length);
                            cameranotes[guitar] = new NoteOnEvent(venue_events.AbsoluteTime, 1, 63, 96, note_length);
                            cameranotes[vocals] = new NoteOnEvent(venue_events.AbsoluteTime, 1, 64, 96, note_length);
                            cameranotes[nobehind] = new NoteOnEvent(venue_events.AbsoluteTime, 1, 70, 96, note_length);
                            cameranotes[onlyfar] = new NoteOnEvent(venue_events.AbsoluteTime, 1, 71, 96, note_length);
                            cameranotes[onlyclose] = new NoteOnEvent(venue_events.AbsoluteTime, 1, 72, 96, note_length);
                            cameranotes[noclose] = new NoteOnEvent(venue_events.AbsoluteTime, 1, 73, 96, note_length);

                            enabled[cameracut] = true; //always enabled for [coop shots

                            //players
                            if (new_event.Contains("all_"))
                            {
                                enabled[drummer] = true;
                                enabled[bass] = true;
                                enabled[guitar] = true;
                                enabled[vocals] = true;
                            }
                            else if (new_event.Contains("front_"))
                            {
                                enabled[bass] = true;
                                enabled[guitar] = true;
                                enabled[vocals] = true;
                            }
                            else if (new_event.Contains("v_") || new_event.Contains("_v"))
                            //this allows for single or duo shots
                            {
                                enabled[vocals] = true;
                            }
                            else if (new_event.Contains("g_") || new_event.Contains("_g"))
                            {
                                enabled[guitar] = true;
                            }
                            else if ((new_event.Contains("b_") || new_event.Contains("_b")) &&
                                     !new_event.Contains("behind"))
                            {
                                enabled[bass] = true;
                            }
                            else if (new_event.Contains("d_") || new_event.Contains("_d"))
                            {
                                enabled[drummer] = true;
                            }
                            else if (new_event.Contains("k_") || new_event.Contains("_k"))
                            {
                                enabled[guitar] = true; //keys not supported
                            }

                            //camera placement
                            if (new_event.Contains("behind"))
                            {
                                enabled[noclose] = true;
                            }
                            else if (new_event.Contains("near") || new_event.Contains("closeup"))
                            {
                                enabled[nobehind] = true;
                                enabled[onlyclose] = true;
                            }
                            else if (new_event.Contains("far"))
                            {
                                enabled[nobehind] = true;
                                enabled[noclose] = true;
                                enabled[onlyfar] = true;
                            }

                            //add the notes that are enabled
                            for (var c = 0; c < 9; c++)
                            {
                                if (!enabled[c]) continue;
                                to_add.Add(cameranotes[c]);
                                to_add.Add(new NoteEvent(cameranotes[c].AbsoluteTime + cameranotes[c].NoteLength, cameranotes[c].Channel, MidiCommandCode.NoteOff, cameranotes[c].NoteNumber, 0));
                            }
                        }
                        else
                        {
                            continue;
                        }
                        to_remove.Add(venue_events);
                    }
                    else if (venue_events.CommandCode == MidiCommandCode.MetaEvent)
                    {
                        var venue_event = (MetaEvent)venue_events;
                        if (venue_event.MetaEventType == MetaEventType.EndTrack)
                        {
                            to_remove.Add(venue_events);
                        }
                    }
                    else switch (venue_events.CommandCode)
                        {
                            case MidiCommandCode.NoteOn:
                                {
                                    var note = (NoteOnEvent)venue_events;
                                    if (note.NoteNumber == 41) //can't have keys spotlight
                                    {
                                        to_remove.Add(note);
                                    }
                                }
                                break;
                            case MidiCommandCode.NoteOff:
                                {
                                    var note = (NoteEvent)venue_events;
                                    if (note.NoteNumber == 41) //can't have keys spotlight
                                    {
                                        to_remove.Add(note);
                                    }
                                }
                                break;
                        }
                }
            }

            if (venue == -1)
            {
                return false;
            }

            foreach (var remove in to_remove)
            {
                rbn2.Events[venue].Remove(remove);
            }
            foreach (var add in to_add)
            {
                rbn2.Events[venue].Add(add);
            }
            rbn2.Events[venue].Add(new MetaEvent(MetaEventType.EndTrack, 0, final_event + (note_length * 2)));

            try
            {
                MidiFile.Export(output, rbn2.Events);
            }
            catch (Exception)
            {
                return false; //if exporting fails
            }
            return true;
        }
    }
}
