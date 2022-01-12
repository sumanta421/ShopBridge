create procedure DeleteProduct
@Id bigint
as
begin
delete from dbo.[Product] where Id=@Id
end