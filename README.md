# location of the api
cd C:\Users\b037\source\repos\run_sql\sqlApi_console\bin\Debug

.\sql.exe help
.\sql.exe servername
.\sql.exe servername set WINDEVAD1051
.\sql.exe servername set winuatad1056

.\sql.exe db
.\sql.exe db set b037_Darta
.\sql.exe db set b037_agl
.\sql.exe db set DartaAnonymise

.\sql.exe cs # connection string
.\sql.exe dir # the directory to search for sql files
.\sql.exe dir set "C:\Users\b037\Documents\SQL Server Management Studio\GDPR\Sps\agents"
.\sql.exe dir set "C:\dev\IPSI\GDPR\IPSI\Databases\RegistryDatabase\RegistryDatabase\Seed Data\pim\Stored Procedures"

.\sql.exe recursive # whether to search all sub directories
.\sql.exe recursive set true
.\sql.exe load # loads the files ready to be run (deletes all existing files)
.\sql.exe load add # adds the files (to current list) ready to be run
.\sql.exe add \pim_MaskLC_Location.sql \pim_MaskPI_PhoneticIndex.sql
.\sql.exe leave \pim_MaskAC_AgentsCommission.sql \pim_MaskBB_BankOrBulkPayer.sql
.\sql.exe leave all # deletes all files (not physically)
.\sql.exe list # list the load list
.\sql.exe run # runs the sql in the list
.\sql.exe change Create alter # alter a number of text files
.\sql.exe show pim_MaskPW_ArchivedClaim.sql
.\sql.exe addsql "END CATCH" "-- PUKE --"
.\sql.exe edit BP_UpdateMaskFlag.sql # edit some SQL
.\sql.exe delSql procedure adelme delme # delete stored procs 
