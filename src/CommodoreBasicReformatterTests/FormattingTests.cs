using System;
using System.IO;
using System.Net;
using CommodoreBasicReformatter;
using Xunit;

namespace CommodoreBasicReformatterTests
{
    public class UnitTest1
    {
        private static string RagingRobotsInput = 
            @"1 REM*********************************
2 REM* RAGING ROBOTS    COMMODORE 64 *
3 REM* BY LARRY HATCH         V10.28 *
4 REM* MENLO PARK  CALIF 94025  1983 *
5 REM*********************************
100 POKE 894,0:POKE895,0:POKE53280,9:POKE53281,0
110 CLR:PRINT"".. RAGING ROBOTS  BY LARRY HATCH""
120 PRINT""V _raging robot.  Q _ our hero.""
130 print""use shift and cursor keys for vertical""
140 print""and horizontal moves."":poke53296,0
150 print""the unshifted       f1   f3""
160 print""function keys         M N""
170 print""are for moves  ==>     Q ""
180 print""digaonally as         N M""
190 print""shown here.         f7   f5""
200 print""the robots aren't very bright and they""
210 print""will chase you across a minefield!""
220 print"" .. . _ minefield.""
230 print""they always move horizontally first.""
240 print"".press any key to play Z"":nr=30
250 vl=54296:ad=54277:p1=54273:p3=54287:wf=54276
260 qa=198:qb=214:sc=1024:cs=55296:pokeqa,0:z$=""""
270 rt=ti:wait qa,1:getz$:pokeqa,0:rq=ti-rt
280 rr=rq*rnd(0):dim r(nr),p(nr):r=nr
290 print""."":fori=1to80:pokevl,5:pokevl,0
300 mf=int(988*rnd(rr))+12:pokesc+mf,102
310 pokecs+mf,5:nexti:fori=1tonr:pokevl,10
320 r(i)=int(988*rnd(rr))+12:if r(i)=500then320
330 poke sc+r(i),214:pokecs+r(i),4:p(i)=1
340 pokevl,0:nexti:yu=500:pokesc+yu,81
350 pokecs+yu,1:pokeqa,0:ys=peek(895)
360 ifr=0thenprint"".you win"":ys=ys+1:goto840
370 print"".your turn  "":pokeqa,0:waitqa,1
380 getd$:ys=peek(895):ifd$=""""then380
390 da=asc(d$):ifda=145thendp=-40:goto480
400 ifda=17 thendp=40 :goto480:rem down
410 ifda=157thendp=-1 :goto480:rem left
420 ifda=29 thendp=1  :goto480:rem right
430 ifda=133thendp=-41:goto480:rem up-left
440 ifda=134thendp=-39:goto480:rem up-right
450 ifda=135thendp=41 :goto480:rem down-right
460 ifda=136thendp=39 :goto480:rem down-left
470 goto360:rem* invalid keystroke so go back
480 yn=yu+dp:ifyn<40oryn>999 then yn=500
490 ifpeek(sc+yn)<>32 thenlc=yu:goto790
500 pokesc+yn,81:pokecs+yn,1:pokesc+yu,32
510 yu=yn:pokeqa,0:tp=10+10*d:gosub900
520 print"".                  "":fori=1tonr
530 ifpeek(sc+r(i))=32thenp(i)=0:goto730
540 ifp(i)=0 goto730
550 x1=r(i)-40*int(r(i)/40)
560 x2=yu-40*int(yu/40):x=x2-x1
570 y1=int((r(i)-1)/40):y2=int((yu-1)/40):y=y2-y1
580 j1=r(i)+sgn(x):d1=peek(sc+j1):pokesc+r(i),32
590 poke sc+j1,214:pokecs+j1,4:r(i)=j1
600 j2=r(i)+40*sgn(y):tp=10+i*2:gosub900
610 d2=peek(sc+j2):ifx=o goto660
620 if d1=81 then p(i)=0:lc=sc+yu:goto790
630 if d1=102then lc=sc+j1:p(i)=0:r=r-1
640 if d1=214then lc=sc+j1:p(i)=0:r=r-2
650 ifd1=102ord1=214thengosub770:goto730
660 ifp(i)=0ory=0 goto730
670 poke sc+j2,214:pokecs+j2,4:pokesc+r(i),32
680 r(i)=j2:tp=30+i*2:gosub900
690 if d2=81 then p(i)=0:lc=sc+yu:goto790
700 if d2=102then lc=sc+j2:p(i)=0:r=r-1
710 if d2=214then lc=sc+j2:p(i)=0:r=r-2
720 ifd2=102ord2=214thengosub770
730 nexti:rc=0:for rt=1to nr
740 ifpeek(sc+r(rt))<>214thenp(rt)=0
750 ifp(rt)<>0thenrc=rc+1
760 nextrt:r=rc:print"".""r"". "":goto360
770 forj=1to12:pokelc,90:pokelc,32:nextj
780 gosub920:print"".""r"". "":return
790 j=0:forj=1to12:pokelc,90:pokelc,32
800 nextj:gosub920:pokesc+yu,32:pokesc+yn,93
810 pokecs+yn,1:pokecs+yn-40,1
820 pokesc+yn-40,91:rs=peek(894)+1
830 print"".truly a pity "":poke894,rs
840 rc=0:poke895,ys:for rt=1tonr
850 ifpeek(sc+r(rt))<>214thenp(rt)=0
860 ifp(rt)<>0thenrc=rc+1
870 nextrt:r=rc:print"".""r"". "":pokeqb,23:print
880 print""hero""peek(895);;""robots""peek(894);
890 clr:goto240:rem*sound subroutines below
900 pokewf,0:pokead,40:pokevl,15:pokep1,tp
910 pokep3,9+peek(162)/9:pokewf,21:return
920 pokewf,0:pokead,7:pokep1,30:rem*explosion
930 forz=15to 0step-1:pokevl,z:pokewf,129
940 forq=0to2:nextq:pokewf,128:nextz:return
";

        private static string RagingRobotsOut = @"1 rem *********************************
2 rem * RAGING ROBOTS    COMMODORE 64 *
3 rem * BY LARRY HATCH         V10.28 *
4 rem * MENLO PARK  CALIF 94025  1983 *
5 rem *********************************
100 poke 894,0 : poke 895,0 : poke 53280,9 : poke 53281,0
110 clr : print "".. RAGING ROBOTS  BY LARRY HATCH""
120 print ""V _raging robot.  Q _ our hero.""
130 print ""use shift and cursor keys for vertical""
140 print ""and horizontal moves."" : poke 53296,0
150 print ""the unshifted       f1   f3""
160 print ""function keys         M N""
170 print ""are for moves  ==>     Q ""
180 print ""digaonally as         N M""
190 print ""shown here.         f7   f5""
200 print ""the robots aren't very bright and they""
210 print ""will chase you across a minefield!""
220 print "" .. . _ minefield.""
230 print ""they always move horizontally first.""
240 print "".press any key to play Z"" : nr = 30
250 vl = 54296 : ad = 54277 : p1 = 54273 : p3 = 54287 : wf = 54276
260 qa = 198 : qb = 214 : sc = 1024 : cs = 55296 : poke qa,0 : z$ = """"
270 rt = ti : wait qa,1 : get z$ : poke qa,0 : rq = ti-rt
280 rr = rq*rnd(0) : dim r(nr),p(nr) : r = nr
290 print ""."" : for i = 1 to 80 : poke vl,5 : poke vl,0
300 mf = int(988*rnd(rr))+12 : poke sc+mf,102
310 poke cs+mf,5 : next i : for i = 1 to nr : poke vl,10
320 r(i)= int(988*rnd(rr))+12 : if r(i)= 500 then 320
330 poke sc+r(i),214 : poke cs+r(i),4 : p(i)= 1
340 poke vl,0 : next i : yu = 500 : poke sc+yu,81
350 poke cs+yu,1 : poke qa,0 : ys = peek(895)
360 if r = 0 then print "".you win"" : ys = ys+1 : goto 840
370 print "".your turn  "" : poke qa,0 : wait qa,1
380 get d$ : ys = peek(895) : if d$ = """" then 380
390 da = asc(d$) : if da = 145 then dp =-40 : goto 480
400 if da = 17 then dp = 40 : goto 480 : rem down
410 if da = 157 then dp =-1 : goto 480 : rem left
420 if da = 29 then dp = 1 : goto 480 : rem right
430 if da = 133 then dp =-41 : goto 480 : rem up-left
440 if da = 134 then dp =-39 : goto 480 : rem up-right
450 if da = 135 then dp = 41 : goto 480 : rem down-right
460 if da = 136 then dp = 39 : goto 480 : rem down-left
470 goto 360 : rem * invalid keystroke so go back
480 yn = yu+dp : if yn<40 or yn>999 then yn = 500
490 if peek(sc+yn)<>32 then lc = yu : goto 790
500 poke sc+yn,81 : poke cs+yn,1 : poke sc+yu,32
510 yu = yn : poke qa,0 : tp = 10+10*d : gosub 900
520 print "".                  "" : for i = 1 to nr
530 if peek(sc+r(i))= 32 then p(i)= 0 : goto 730
540 if p(i)= 0 goto 730
550 x1 = r(i)-40*int(r(i)/40)
560 x2 = yu-40*int(yu/40) : x = x2-x1
570 y1 = int((r(i)-1)/40) : y2 = int((yu-1)/40) : y = y2-y1
580 j1 = r(i)+sgn(x) : d1 = peek(sc+j1) : poke sc+r(i),32
590 poke sc+j1,214 : poke cs+j1,4 : r(i)= j1
600 j2 = r(i)+40*sgn(y) : tp = 10+i*2 : gosub 900
610 d2 = peek(sc+j2) : if x = o goto 660
620 if d1 = 81 then p(i)= 0 : lc = sc+yu : goto 790
630 if d1 = 102 then lc = sc+j1 : p(i)= 0 : r = r-1
640 if d1 = 214 then lc = sc+j1 : p(i)= 0 : r = r-2
650 if d1 = 102 or d1 = 214 then gosub 770 : goto 730
660 if p(i)= 0 or y = 0 goto 730
670 poke sc+j2,214 : poke cs+j2,4 : poke sc+r(i),32
680 r(i)= j2 : tp = 30+i*2 : gosub 900
690 if d2 = 81 then p(i)= 0 : lc = sc+yu : goto 790
700 if d2 = 102 then lc = sc+j2 : p(i)= 0 : r = r-1
710 if d2 = 214 then lc = sc+j2 : p(i)= 0 : r = r-2
720 if d2 = 102 or d2 = 214 then gosub 770
730 next i : rc = 0 : for rt = 1 to nr
740 if peek(sc+r(rt))<>214 then p(rt)= 0
750 if p(rt)<>0 then rc = rc+1
760 next rt : r = rc : print ""."" r "". "" : goto 360
770 for j = 1 to 12 : poke lc,90 : poke lc,32 : next j
780 gosub 920 : print ""."" r "". "" : return
790 j = 0 : for j = 1 to 12 : poke lc,90 : poke lc,32
800 next j : gosub 920 : poke sc+yu,32 : poke sc+yn,93
810 poke cs+yn,1 : poke cs+yn-40,1
820 poke sc+yn-40,91 : rs = peek(894)+1
830 print "".truly a pity "" : poke 894,rs
840 rc = 0 : poke 895,ys : for rt = 1 to nr
850 if peek(sc+r(rt))<>214 then p(rt)= 0
860 if p(rt)<>0 then rc = rc+1
870 next rt : r = rc : print ""."" r "". "" : poke qb,23 : print
880 print ""hero"" peek(895);;""robots"" peek(894);
890 clr : goto 240 : rem *sound subroutines below
900 poke wf,0 : poke ad,40 : poke vl,15 : poke p1,tp
910 poke p3,9+peek(162)/9 : poke wf,21 : return
920 poke wf,0 : poke ad,7 : poke p1,30 : rem *explosion
930 for z = 15 to 0 step-1 : poke vl,z : poke wf,129
940 for q = 0 to 2 : next q : poke wf,128 : next z : return
";

        private static string RagingRobotsRenumberedOut = @"1 rem *********************************
2 rem * RAGING ROBOTS    COMMODORE 64 *
3 rem * BY LARRY HATCH         V10.28 *
4 rem * MENLO PARK  CALIF 94025  1983 *
5 rem *********************************
100 poke 894,0
101 poke 895,0
102 poke 53280,9
103 poke 53281,0
110 clr
111 print "".. RAGING ROBOTS  BY LARRY HATCH""
120 print ""V _raging robot.  Q _ our hero.""
130 print ""use shift and cursor keys for vertical""
140 print ""and horizontal moves.""
141 poke 53296,0
150 print ""the unshifted       f1   f3""
160 print ""function keys         M N""
170 print ""are for moves  ==>     Q ""
180 print ""digaonally as         N M""
190 print ""shown here.         f7   f5""
200 print ""the robots aren't very bright and they""
210 print ""will chase you across a minefield!""
220 print "" .. . _ minefield.""
230 print ""they always move horizontally first.""
240 print "".press any key to play Z""
241 nr = 30
250 vl = 54296
251 ad = 54277
252 p1 = 54273
253 p3 = 54287
254 wf = 54276
260 qa = 198
261 qb = 214
262 sc = 1024
263 cs = 55296
264 poke qa,0
265 z$ = """"
270 rt = ti
271 wait qa,1
272 get z$
273 poke qa,0
274 rq = ti-rt
280 rr = rq*rnd(0)
281 dim r(nr),p(nr)
282 r = nr
290 print "".""
291 for i = 1 to 80
292 poke vl,5
293 poke vl,0
300 mf = int(988*rnd(rr))+12
301 poke sc+mf,102
310 poke cs+mf,5
311 next i
312 for i = 1 to nr
313 poke vl,10
320 r(i)= int(988*rnd(rr))+12
321 if r(i)= 500 then 320
330 poke sc+r(i),214
331 poke cs+r(i),4
332 p(i)= 1
340 poke vl,0
341 next i
342 yu = 500
343 poke sc+yu,81
350 poke cs+yu,1
351 poke qa,0
352 ys = peek(895)
360 if r = 0 then print "".you win""
361 ys = ys+1
362 goto 840
370 print "".your turn  ""
371 poke qa,0
372 wait qa,1
380 get d$
381 ys = peek(895)
382 if d$ = """" then 380
390 da = asc(d$)
391 if da = 145 then dp =-40
392 goto 480
400 if da = 17 then dp = 40 : rem down
401 goto 480
410 if da = 157 then dp =-1 : rem left
411 goto 480
420 if da = 29 then dp = 1 : rem right
421 goto 480
430 if da = 133 then dp =-41 : rem up-left
431 goto 480
440 if da = 134 then dp =-39 : rem up-right
441 goto 480
450 if da = 135 then dp = 41 : rem down-right
451 goto 480
460 if da = 136 then dp = 39 : rem down-left
461 goto 480
470 goto 360 : rem * invalid keystroke so go back
480 yn = yu+dp
481 if yn<40 or yn>999 then yn = 500
490 if peek(sc+yn)<>32 then lc = yu
491 goto 790
500 poke sc+yn,81
501 poke cs+yn,1
502 poke sc+yu,32
510 yu = yn
511 poke qa,0
512 tp = 10+10*d
513 gosub 900
520 print "".                  ""
521 for i = 1 to nr
530 if peek(sc+r(i))= 32 then p(i)= 0
531 goto 730
540 if p(i)= 0 goto 730
550 x1 = r(i)-40*int(r(i)/40)
560 x2 = yu-40*int(yu/40)
561 x = x2-x1
570 y1 = int((r(i)-1)/40)
571 y2 = int((yu-1)/40)
572 y = y2-y1
580 j1 = r(i)+sgn(x)
581 d1 = peek(sc+j1)
582 poke sc+r(i),32
590 poke sc+j1,214
591 poke cs+j1,4
592 r(i)= j1
600 j2 = r(i)+40*sgn(y)
601 tp = 10+i*2
602 gosub 900
610 d2 = peek(sc+j2)
611 if x = o goto 660
620 if d1 = 81 then p(i)= 0
621 lc = sc+yu
622 goto 790
630 if d1 = 102 then lc = sc+j1
631 p(i)= 0
632 r = r-1
640 if d1 = 214 then lc = sc+j1
641 p(i)= 0
642 r = r-2
650 if d1 = 102 or d1 = 214 then gosub 770
651 goto 730
660 if p(i)= 0 or y = 0 goto 730
670 poke sc+j2,214
671 poke cs+j2,4
672 poke sc+r(i),32
680 r(i)= j2
681 tp = 30+i*2
682 gosub 900
690 if d2 = 81 then p(i)= 0
691 lc = sc+yu
692 goto 790
700 if d2 = 102 then lc = sc+j2
701 p(i)= 0
702 r = r-1
710 if d2 = 214 then lc = sc+j2
711 p(i)= 0
712 r = r-2
720 if d2 = 102 or d2 = 214 then gosub 770
730 next i
731 rc = 0
732 for rt = 1 to nr
740 if peek(sc+r(rt))<>214 then p(rt)= 0
750 if p(rt)<>0 then rc = rc+1
760 next rt
761 r = rc
762 print ""."" r "". ""
763 goto 360
770 for j = 1 to 12
771 poke lc,90
772 poke lc,32
773 next j
780 gosub 920
781 print ""."" r "". ""
782 return
790 j = 0
791 for j = 1 to 12
792 poke lc,90
793 poke lc,32
800 next j
801 gosub 920
802 poke sc+yu,32
803 poke sc+yn,93
810 poke cs+yn,1
811 poke cs+yn-40,1
820 poke sc+yn-40,91
821 rs = peek(894)+1
830 print "".truly a pity ""
831 poke 894,rs
840 rc = 0
841 poke 895,ys
842 for rt = 1 to nr
850 if peek(sc+r(rt))<>214 then p(rt)= 0
860 if p(rt)<>0 then rc = rc+1
870 next rt
871 r = rc
872 print ""."" r "". ""
873 poke qb,23
874 print
880 print ""hero"" peek(895);;""robots"" peek(894);
890 clr : rem *sound subroutines below
891 goto 240
900 poke wf,0
901 poke ad,40
902 poke vl,15
903 poke p1,tp
910 poke p3,9+peek(162)/9
911 poke wf,21
912 return
920 poke wf,0 : rem *explosion
921 poke ad,7
922 poke p1,30
930 for z = 15 to 0 step-1
931 poke vl,z
932 poke wf,129
940 for q = 0 to 2
941 next q
942 poke wf,128
943 next z
944 return
";
        [Fact]
        public void Reformat()
        {
            var output = new Reformatter().Reformat(RagingRobotsInput, false);
            Assert.Equal(RagingRobotsOut, output);
        }

        [Fact]
        public void ReformatAndSplitLines()
        {
            var output = new Reformatter().Reformat(RagingRobotsInput, true);
            Assert.Equal(RagingRobotsRenumberedOut, output);
        }

        [Fact]
        public void ReformatCodeWithNoSpaces()
        {
            var program = @"10 a=10
20 b=15
30 fori=atob
35 printi
44 nexti
";
            var output = new Reformatter().Reformat(program, false);
            Assert.Equal(@"10 a = 10
20 b = 15
30 for i = a to b
35 print i
44 next i
", output);
        }
    }
}
