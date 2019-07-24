using System.Collections.Generic;
using System.Linq;

namespace CommodoreBasicReformatter.Explain
{
    class MemoryExplainer : IExplainer
    {
        static readonly string AddressExplanations = 
@"19 | Current I/O device number
20-21 | Line number during GOSUB, GOTO and RUN. Mem adr during PEEK, POKE, SYS and WAIT
23-24 | Pointer to previous expression in string stack
25-33 | String stack, temporary area for processing string expressions
34-37 | Temporary area for various operations
38-41 | Auxiliary arithmetical register for division and multiplication
43-44 | Pointer to beginning of BASIC area
45-46 | Pointer to beginning of variable area. (End of program plus 1.)
47-48 | Pointer to beginning of array variable area
49-50 | Pointer to end of array variable area
51-52 | Pointer to beginning of string variable area. 
53-54 | Pointer to memory allocated for current string variable
55-56 | Pointer to end of BASIC area
57-58 | Current BASIC line number
59-60 | Current BASIC line number for CONT
61-62 | Pointer to next BASIC instruction for CONT. 
63-64 | BASIC line number of current DATA item for READ
65-66 | Pointer to next DATA item for READ
67-68 | Pointer to AddressExplanations result during GET, INPUT and READ
69-70 | Name and type of current variable. 
71-72 | Pointer to value of current variable or FN function
73-74 | Pointer to value of current variable during LET. 
77 | Comparison operator indicator
78-79 | Pointer to current FN function
80-81 | Pointer to current string variable during memory allocation
83 | Step size of garbage collection
84-86 | JMP ABS machine instruction, jump to current BASIC function
87-91 | Arithmetic register #3 
92-96 | Arithmetic register #4 
97-101 | FAC, arithmetic register #1 
102 | Sign of FAC. 
103 | Number of degrees during polynomial evaluation
104 | Temporary area for various operations
105-109 | ARG, arithmetic register #2
110 | Sign of ARG
111-112 | Pointer to first string expression during string comparison
113-114 | Auxiliary pointer during array operations. 
115-138 | CHRGET. Machine code routine to read next byte from BASIC program or direct command 
139-143 | Previous result of RND()
144 | Value of ST variable, device status for serial bus and datasette AddressExplanations/output. 
145 | Stop key indicator
147 | LOAD/VERIFY switch
148 | Serial bus output cache status
149 | Serial bus output cache, previous byte to be sent to serial bus
151 | Temporary area for saving original value of Y register during AddressExplanations from RS232. 
152 | Number of files currently open
153 | Current AddressExplanations device number
154 | Current output device number
157 | System error display switch
158 | Byte to be put into output buffer during RS232 and datasette output. 
159 | Auxiliary counter for writing file name into datasette buffer. 
160-162 | Value of TI variable, time of day, increased by 1 every 1/60 second
163 | EOI switch during serial bus output
164 | Byte buffer during serial bus AddressExplanations. 
165 | Bit counter during serial bus AddressExplanations/output. 
166 | Offset of current byte in datasette buffer
167 | Bit buffer during RS232 AddressExplanations
168 | Bit counter during RS232 AddressExplanations
169 | Stop bit switch during RS232 AddressExplanations
170 | Byte buffer during RS232 AddressExplanations
171 | Parity during RS232 AddressExplanations. 
172-173 | Start address for SAVE to serial bus. 
174-175 | Load address read from AddressExplanations file and pointer to current byte during LOAD/VERIFY from serial bus
178-179 | Pointer to datasette buffer
180 | Bit counter and stop bit switch during RS232 output
181 | bit buffer during RS232 output
182 | Byte buffer during RS232 output
183 | Length of file name or disk command
184 | Logical number of current file
185 | Secondary address of current file
186 | Device number of current file
187-188 | Pointer to current file name or disk command
189 | Parity during RS232 output. Byte buffer during datasette AddressExplanations/output
190 | Block counter during datasette AddressExplanations/output
192 | Datasette motor switch. 
193-194 | Start address during SAVE to serial bus, LOAD and VERIFY from datasette and SAVE to datasette. 
195-196 | Start address for a secondary address of 0 for LOAD and VERIFY from serial bus or datasette. 
197 | Matrix code of key previously pressed. 
198 | Length of keyboard buffer
199 | Reverse mode switch. 
200 | Length of line minus 1 during screen AddressExplanations. 
201 | Cursor row during screen AddressExplanations
202 | Cursor column during screen AddressExplanations
203 | Matrix code of key currently being pressed. 
204 | Cursor visibility switch
205 | Delay counter for changing cursor phase. 
206 | Screen code of character under cursor
207 | Cursor phase switch. 
208 | End of line switch during screen AddressExplanations. 
209-210 | Pointer to current line in screen memory
211 | Current cursor column. 
212 | Quotation mode switch. Values:
213 | Length of current screen line minus 1. 
214 | Current cursor row. 
215 | PETSCII code of character during screen AddressExplanations/output. 
217-241 | High byte of pointers to each line in screen memory. 
242 | Temporary area during scrolling the screen
243-244 | Pointer to current line in Color RAM
245-246 | Pointer to current conversion table during conversion from keyboard matrix codes to PETSCII
247-248 | Pointer to RS232 AddressExplanations buffer. 
249-250 | Pointer to RS232 output buffer. 
255-266 | Buffer for conversion from floating point to string
256-317 | Pointers to bytes read with error during datasette AddressExplanations. 
256-511 | Processor stack. Also used for storing data related to FOR and GOSUB
512-600 | Input buffer, storage area for data read from screen
601-610 | Logical numbers assigned to files
611-620 | Device numbers assigned to files 
621-630 | Secondary addresses assigned to files 
631-640 | Keyboard buffer 
641-642 | Pointer to beginning of BASIC area after memory test. 
643-644 | Pointer to end of BASIC area after memory test. 
645 | Unused. (Serial bus timeout.)
646 | Current cursor color
647 | Color of character under cursor
648 | High byte of pointer to screen memory for screen AddressExplanations/output
649 | Maximum length of keyboard buffer
650 | Keyboard repeat switch 
651 | Delay counter during repeat
652 | Repeat sequence delay counter, for delaying before first repetition. 
653 | Shift key indicator. 
654 | Previous value of shift key indicator. 
655-656 | Execution address of routine that, based on the status of shift keys
657 | Commodore-Shift switch. 
658 | Scroll direction switch during scrolling the screen. 
659 | RS232 control register. 
660 | RS232 command register
661-662 | Default value of RS232 output timer, based on baud rate. 
663 | Value of ST variable, device status for RS232 AddressExplanations/output. 
664 | RS232 byte size, number of data
665-666 | Default value of RS232 AddressExplanations timer, based on baud rate. 
667 | Offset of byte received in RS232 AddressExplanations buffer
668 | Offset of current byte in RS232 AddressExplanations buffer
669 | Offset of byte to send in RS232 output buffer
670 | Offset of current byte in RS232 output buffer
671-672 | Temporary area for saving pointer to original interrupt service routine during datasette AddressExplanations output
673 | Temporary area for saving original value of CIA#2 interrupt
674 | Temporary area for saving original value of CIA#1 timer #1
677 | Number of line currently being scrolled during scrolling the screen
678 | PAL/NTSC switch, for selecting RS232 baud rate from the proper table. 
768-769 | Execution address of warm reset. 
770-771 | Execution address of BASIC idle loop. 
772-773 | Execution address of BASIC line tokenizater routine
774-775 | Execution address of BASIC token decoder routine
776-777 | Execution address of BASIC instruction executor routine
778-779 | Execution address of routine reading next item of BASIC expression
780 | Default value of register A for SYS. Value of register A after SYS
781 | Default value of register X for SYS. Value of register X after SYS
782 | Default value of register Y for SYS. Value of register Y after SYS
783 | Default value of status register for SYS. Value of status register after SYS
784-786 | JMP ABS machine instruction, jump to USR() function. 
788-789 | Execution address of interrupt service routine
790-791 | Execution address of BRK service routine
792-793 | Execution address of non-maskable interrupt service routine
794-795 | Execution address of OPEN, routine opening files
796-797 | Execution address of CLOSE, routine closing files
798-799 | Execution address of CHKIN, routine defining file as default AddressExplanations
800-801 | Execution address of CHKOUT, routine defining file as default output
802-803 | Execution address of CLRCHN, routine initializating AddressExplanations/output
804-805 | Execution address of CHRIN, data AddressExplanations routine, except for keyboard and RS232 AddressExplanations
806-807 | Execution address of CHROUT, general purpose data output routine
808-809 | Execution address of STOP
810-811 | Execution address of GETIN, general purpose data AddressExplanations routine
812-813 | Execution address of CLALL, routine initializing AddressExplanations/output and clearing all file assignment tables
816-817 | Execution address of LOAD, routine loading files
818-819 | Execution address of SAVE, routine saving files
828-1019 | Datasette buffer
1024-2023 | Default area of screen memory
2040-2047 | Default area for sprite pointers
2048 | Unused. (Must contain a value of 0 so that the BASIC program can be RUN.)
53248 | Sprite #0 X-coordinate
53249 | Sprite #0 Y-coordinate
53250 | Sprite #1 X-coordinate
53251 | Sprite #1 Y-coordinate
53252 | Sprite #2 X-coordinate
53253 | Sprite #2 Y-coordinate
53254 | Sprite #3 X-coordinate
53255 | Sprite #3 Y-coordinate
53256 | Sprite #4 X-coordinate
53257 | Sprite #4 Y-coordinate
53258 | Sprite #5 X-coordinate
53259 | Sprite #5 Y-coordinate
53260 | Sprite #6 X-coordinate
53261 | Sprite #6 Y-coordinate
53262 | Sprite #7 X-coordinate
53263 | Sprite #7 Y-coordinate
53264 | Sprite #0-#7 X-coordinates
53265 | Screen control register #1
53266 | Read: Current raster line. 
53267 | Light pen X-coordinate
53268 | Light pen Y-coordinate
53269 | Sprite enable register
53270 | Screen control register #2
53271 | Sprite double height register
53272 | Memory setup register. 
53273 | Interrupt status register. Read
53274 | Interrupt control register
53275 | Sprite priority register
53276 | Sprite multicolor mode register
53277 | Sprite double width register
53278 | Sprite-sprite collision register
53279 | Sprite-background collision register. 
53280 | Border color
53281 | Background color
53282 | Extra background color #1
53283 | Extra background color #2
53284 | Extra background color #3
53285 | Sprite extra color #1
53286 | Sprite extra color #1
53287 | Sprite #0 color
53288 | Sprite #1 color
53289 | Sprite #2 color
53290 | Sprite #3 color
53291 | Sprite #4 color
53292 | Sprite #5 color
53293 | Sprite #6 color
53294 | Sprite #7 color
53312-54271 | VIC-II register images
54272-54273 | Voice #1 frequency
54274-54275 | Voice #1 pulse width
54276 | Voice #1 control register
54277 | Voice #1 Attack and Decay length
54278 | Voice #1 Sustain volume and Release length. 54279-54280 | Voice #2 frequency
54281-54282 | Voice #2 pulse width
54283 | Voice #2 control register
54284 | Voice #2 Attack and Decay length
54285 | Voice #2 Sustain volume and Release length
54286-54287 | Voice #3 frequency
54288-54289 | Voice #3 pulse width
54290 | Voice #3 control register
54291 | Voice #3 Attack and Decay length
54292 | Voice #3 Sustain volume and Release length
54293 | Filter cut off frequency
54294 | Filter cut off frequency
54295 | Filter control. 
54296 | Volume and filter modes
54297 | X value of paddle selected at memory address $DD00
54298 | Y value of paddle selected at memory address $DD00
54299 | Voice #3 waveform output
54300 | Voice #3 ADSR output
54304-55295 | SID register images
55296-56295 | Color RAM
56320 | Port A, keyboard matrix columns and joystick #2. 
56321 | Port B, keyboard matrix rows and joystick #1. 
56322 | Port A data direction register
56323 | Port B data direction register
56324-56325 | Timer A. 
56326-56327 | Timer B. 
56328 | Time of Day, tenth seconds (in BCD). 
56329 | Time of Day, seconds (in BCD)
56330 | Time of Day, minutes (in BCD)
56331 | Time of Day, hours (in BCD). 
56332 | Serial shift register. 
56333 | Interrupt control and status register. 
56334 | Timer A control register
56335 | Timer B control register
56336-56575 | CIA#1 register images
56576 | Port A, serial bus access
56577 | Port B, RS232 access. 
56578 | Port A data direction register
56579 | Port B data direction register
56580-56581 | Timer A. 
56582-56583 | Timer B. 
56584 | Time of Day, tenth seconds (in BCD). 
56585 | Time of Day, seconds (in BCD)
56586 | Time of Day, minutes (in BCD)
56587 | Time of Day, hours (in BCD). 
56588 | Serial shift register. 
56589 | Interrupt control and status register. 
56590 | Timer A control register
56591 | Timer B control register
56592-56831 | CIA#2 register images
56832-57087 | I/O Area #1, memory mapped registers or machine code routines of optional external devices. 
57088-57343 | I/O Area #2, memory mapped registers or machine code routines of optional external devices. 
65530-65531 | Execution address of non-maskable interrupt service routine
65532-65533 | Execution address of cold reset
65534-65535 | Execution address of interrupt service routine";

        static readonly Dictionary<string, string> C64MemoryMap = new ExplanationsConfigurationParser()
            .BuildExplanations(AddressExplanations);

        static readonly HashSet<string> KeywordUsingAddress = new HashSet<string>() { "wait", "poke", "peek", "sys" };

        public void AddExplanations(GrammarProgram program)
        {
            var variablesAsAdresses = FindVariablesUsedAsAddresses(program);

            foreach (var line in program.Lines)
            {
                for (int stmtCnt = 0; stmtCnt < line.Stmts.Count; stmtCnt++)
                {
                    var block = line.Stmts[stmtCnt];

                    AnalyseVariableAssignment(block, variablesAsAdresses, line);

                    AnalyzeDirectAddressArgTo_PeekPokeWaitSys(block, line);
                }
            }
        }

        static void AnalyzeDirectAddressArgTo_PeekPokeWaitSys(GrammarStmt block, GrammarLine line)
        {
            for (int p = 0; p < block.Content.Count - 1; p++)
            {
                var current = block.Content[p];
                int offset = current.Value == "peek" ? 2 : 1;
                var next = block.Content[p + offset];

                if (current.Type == TokenKind.Keyword
                    && KeywordUsingAddress.Contains(current.Value)
                    && next.Type == TokenKind.Digit
                    && C64MemoryMap.ContainsKey(next.Value))
                {
                    line.Explanations.Add($"{next}={C64MemoryMap[next.Value]}");
                }
            }
        }

        static void AnalyseVariableAssignment(GrammarStmt block, ILookup<string, Token> variablesAsAdresses, GrammarLine line)
        {
            var variableAssignmentOfVariableUsedAsAddress =
                block.Content[0].Type == TokenKind.Name && variablesAsAdresses.Contains(block.Content[0].Value);

            if (variableAssignmentOfVariableUsedAsAddress)
            {
                line.Explanations
                    .AddRange(block.Content.Skip(1)
                        .Where(x => x.Type == TokenKind.Digit)
                        .Where(x => C64MemoryMap.ContainsKey(x.Value))
                        .Select(x => $"{x}={C64MemoryMap[x.Value]}"));
            }

            var forLoopVariableAssignment =
                (block.Content[0].Type == TokenKind.Keyword && block.Content[0].Value == "for")
                && variablesAsAdresses.Contains(block.Content[1].Value);

            if (forLoopVariableAssignment)
            {
                line.Explanations
                    .AddRange(block.Content.Skip(2)
                        .Where(x => x.Type == TokenKind.Digit)
                        .Where(x => C64MemoryMap.ContainsKey(x.Value))
                        .Select(x => $"{x}={C64MemoryMap[x.Value]}"));
            }
        }

        static ILookup<string, Token> FindVariablesUsedAsAddresses(GrammarProgram program)
        {
            var tokens = program.Lines
                .SelectMany(x => x.Stmts)
                .SelectMany(x => x.Content)
                .ToArray();

            var variablesAsAdresses =
                tokens
                    .Select((t, i) =>
                    {
                        int offset = t.Value == "peek" ? 2 : 1;
                        if (t.Type == TokenKind.Keyword
                            && KeywordUsingAddress.Contains(t.Value)
                            && tokens[i + offset].Type == TokenKind.Name)
                        {
                            return tokens[i + offset];
                        }

                        return null;
                    })
                    .Where(x => x != null)
                    .Distinct()
                    .ToLookup(x => x.Value);
            return variablesAsAdresses;
        }
    }
}
