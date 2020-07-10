USE DHPO
Select top 1

(select count(*)  from priorrequesttransactions with (nolock) 
where convert(date,transactiondate) = convert(date,getdate()))as 'DHPO-Total_PR',


(select count(*)   from priorrequesttransactions with (nolock) 
where convert(date,transactiondate) = convert(date,getdate())
and  downloadeddate is null)as 'DHPO-PR_NotDownloaded',


(select count(*)  from PriorAuthorizationTransaction with (nolock) 
where convert(date,transactiondate) = convert(date,getdate())) as 'DHPO-Total_PA' ,


(select count(*)   from PriorAuthorizationTransaction with (nolock) 
where convert(date,transactiondate) = convert(date,getdate())
and  downloadeddate is  null ) as 'DHPO-PA_NotDownloaded',


(select count(*)   from SubmissionTransactions with (nolock) 
where convert(date,transactiondate) = convert(date,getdate())) as 'DHPO-Total_CS',


(select count(*)  from SubmissionTransactions with (nolock) 
where convert(date,transactiondate) = convert(date,getdate())
and  downloadeddate is  null ) as 'DHPO-CS_NotDownloaded'

