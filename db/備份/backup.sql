USE master
GO
DECLARE @backupTime VARCHAR(20)
DECLARE @sqlCommand NVARCHAR(1000)
--設定檔名的執行時間.例如以下的@backupTime將會是200904221156(yyyyMMddHHmm)
--此值可以視需求進行調整,如果是每小時備份,就只要2009042211(yyyyMMddHH)
SELECT @backupTime=(CONVERT(VARCHAR(8), GETDATE(), 112)
         +REPLACE(CONVERT(VARCHAR(5), GETDATE(), 114), ':', ''))
--設定LYTDB資料庫的備份命令
--可視需要修改備份檔存放的位置
SET @sqlCommand = 'BACKUP DATABASE Communication TO DISK=''D:\app\Database_Backup\Communication_'
                  + @backupTime+'.bak'''
EXECUTE sp_executesql @sqlCommand  
GO