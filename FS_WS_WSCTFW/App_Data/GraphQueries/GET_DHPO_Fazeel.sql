USE DHPO

select 'DHPO-Total_PR',count(*)as 'Total_PR_Count'  from priorrequesttransactions with (nolock) 
where convert(date,transactiondate) = convert(date,getdate())
union

select 'DHPO-PR_NotDownloaded',count(*)  from priorrequesttransactions with (nolock) 
where convert(date,transactiondate) = convert(date,getdate())
and  downloadeddate is null 
union

select 'DHPO-Total_PA',count(*)  from PriorAuthorizationTransaction with (nolock) 
where convert(date,transactiondate) = convert(date,getdate())
union

select 'DHPO-PA_NotDownloaded',count(*)  from PriorAuthorizationTransaction with (nolock) 
where convert(date,transactiondate) = convert(date,getdate())
and  downloadeddate is  null 
union

select 'DHPO-Total_CS',count(*)  from SubmissionTransactions with (nolock) 
where convert(date,transactiondate) = convert(date,getdate())
union

select 'DHPO-CS_NotDownloaded',count(*) from SubmissionTransactions with (nolock) 
where convert(date,transactiondate) = convert(date,getdate())
and  downloadeddate is  null 
GO
