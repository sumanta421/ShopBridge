CREATE procedure [AddOrEditProduct]
@Id bigint=null,
@Price decimal(30)=null,
@Name nVarchar(255)=null,
@Description nVarchar(255)=null

as 
BEGIN
if @Id is null
	begin
	Insert into [dbo].[Product] values(@Name,@Price,@Description)
	end
else
	Begin
	Update dbo.Product set [Name]=Coalesce(@Name,[Name]),Price=Coalesce(@Price,Price),[Description]=Coalesce(@Description,[Description]) where Id=@Id
	End
END