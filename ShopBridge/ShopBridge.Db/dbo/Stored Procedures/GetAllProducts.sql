create procedure GetAllProducts
@PageSize int=null,
@PageNumber int=null
as 
BEGIN
if (@PageSize is not null)
begin
select [Name],[Description],[Price] from dbo.Product order by Id Offset (@PageNumber-1)*@PageSize rows Fetch next @PageSize rows only
end
else
begin
select [Name],[Description],[Price] from dbo.Product
end
END