Declare @TranCount int
Set @TranCount = 5000

declare @Payerid int
set @Payerid = 7 

declare @region int
set @region = 3

Declare @TransactionID nvarchar(100)
Declare @Server varchar (100)
set @Server = '10.156.176.85'
Declare @DBName varchar(100)
set @DBName = 'PBMM'
declare @loginId varchar(100)
set @loginId = 'fsheikh' 
declare @Password varchar(100)
set @Password = 'Dell@100'

------------------------------------------------------------------------------------------------------------------------
--select top 100 * from REGION
--select top 100 * from PROVIDER
USE [PBMM]

DECLARE TransList CURSOR 
  LOCAL STATIC READ_ONLY FORWARD_ONLY
FOR 

Select batch_id 
FROM
PBMM..BATCH_DOWNLOAD_TRANS BD with (nolock)
where bd.payer_id = 7
and bd.batch_id in ('26396df4-6107-44d5-8925-a55d39b07d63','e86ef82d-44be-4af3-ae1b-b2cc25025b3b','48b1ebe7-9d87-48e9-a236-8b5f68489a57','710bb64e-0d15-40a3-b093-d2b69938b92d')

---------------------------------------------

open TransList 
FETCH NEXT FROM Translist INTO @TransactionID


WHILE @@FETCH_STATUS = 0
BEGIN 
    
    PRINT @TransactionID
   
   -------------------------------------------------

 
declare @SQL varchar(max)
set @SQL = 
' Select '+
' BD.batch_id as ''BatchId'','+
' BD.transaction_type as ''TransactionType'','+
' BD.region as ''Region'','+
' Convert(varchar(10),BD.date_time,103) +'' ''+ Convert(varchar(5),BD.date_time,8) as ''Date'','+
' BD.is_downloaded as ''IsDownloaded'','+
' BD.payer_id as ''PayerID'' '+
' from PBMM..BATCH_DOWNLOAD_TRANS BD with (NoLock) '+
' where BD.batch_id = '''+cast(@TransactionID as varchar(1000))+''''+
' FOR XML PATH(''''),ROOT(''BatchDownloadTransaction'')'



declare @filename varchar(1000)
set @filename = ''
set @filename = 'C:\TMP\omar\Batch\BDT_' + cast(@transactionID as varchar(1000))+ '.XML'

select @SQL
--sqlcmd -S @server -U loginid -P @LoginID -d @DbName -Q @SQL -o @filename
--set @sql = 'select getdate()'
declare @BCP1 varchar(8000) 
 
set @bcp1 = 'bcp "'+@SQL+'" QUERYOUT "'+@filename+'" -C -c -T -t,'
--'bcp '+ '" select getdate ()"' + ' QUERYOUT "C:\tmp\FILE_'+Cast(12 AS Varchar)+'.CSV" -C -c -T'
--select @BCP1 
--' bcp "'+ @sql  +'" queryout "' + @filename +'" -C -c -T  '
--	select @BCP1
EXEC xp_cmdshell @BCP1
   ----------------------------------------------------

   
   
    FETCH NEXT FROM Translist INTO @TransactionID

END
CLOSE TransList
DEALLOCATE TransList
