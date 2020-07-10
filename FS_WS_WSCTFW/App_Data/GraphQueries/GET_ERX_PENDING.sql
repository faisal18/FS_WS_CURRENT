USE ERX
select 
(select COUNT(*) from [Transaction] with(nolock) where [Status] = 3
and convert(date,dateordered) = convert(date,getdate())
and [Plan] in (
(
	SELECT ID FROM [Plan]
	WHERE Payer IN
	(
		select id from Payer where CodeDha IN
		('INS017','INS013','INS038','INS012','TPA017','INS005','INS020',
		'TPA014','TPA022','TPA029','TPA016','TPA026','TPA025','INS028')
	) 
	and Active = 1)
)) as 'ERX_Payers_Pending_Count',
(select COUNT(*) from [Transaction] with(nolock) where [Status] = 3
and convert(date,dateordered) = convert(date,getdate())
and [Plan] in (
(
	SELECT ID FROM [Plan]
	WHERE Payer NOT IN
	(
		select id from Payer where CodeDha IN
		('INS017','INS013','INS038','INS012','TPA017','INS005','INS020',
		'TPA014','TPA022','TPA029','TPA016','TPA026','TPA025','INS028')
	) 
	and Active = 1)
)) as 'ERX_NonPayers_Pending_Count'