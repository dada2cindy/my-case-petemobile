USE master
GO
DECLARE @backupTime VARCHAR(20)
DECLARE @sqlCommand NVARCHAR(1000)
--�]�w�ɦW������ɶ�.�Ҧp�H�U��@backupTime�N�|�O200904221156(yyyyMMddHHmm)
--���ȥi�H���ݨD�i��վ�,�p�G�O�C�p�ɳƥ�,�N�u�n2009042211(yyyyMMddHH)
SELECT @backupTime=(CONVERT(VARCHAR(8), GETDATE(), 112)
         +REPLACE(CONVERT(VARCHAR(5), GETDATE(), 114), ':', ''))
--�]�wLYTDB��Ʈw���ƥ��R�O
--�i���ݭn�ק�ƥ��ɦs�񪺦�m
SET @sqlCommand = 'BACKUP DATABASE Communication TO DISK=''D:\app\Database_Backup\Communication_'
                  + @backupTime+'.bak'''
EXECUTE sp_executesql @sqlCommand  
GO