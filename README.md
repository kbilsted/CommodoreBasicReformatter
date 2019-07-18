# CommodoreBasicReformatter

# Reformat T64 files

before reformatting you need to convert the tape file to a disc image, then run `petcat` on it.

An example with a T64 file named `RAGINGRO.T64` containing the file `raging robots`

    .\c1541.exe -format diskname,id d64 my_diskimage.d64 -attach my_diskimage.d64
    .\c1541.exe -attach my_diskimage.d64 -tape RAGINGRO.T64
    .\c1541.exe -attach my_diskimage.d64 -extract
    .\petcat.exe -o rr.ascii "raging robots"
    dotnet C:\CommodoreBasicReformatter CommodoreBasicReformatter.dll ".\rr.ascii" rr2.ascii


to make an executable of the re-formatted program run

    .\petcat.exe -w2 -l 0x0801 -o rr2.prg rr2.ascii
	x64.exe rr2.prg

