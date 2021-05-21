select top 1 id,Name,Password,Phone,Email from Users where name='1' and password='1'
select * from [dbo].[Roles]
SELECT *From Users;

UPDATE [dbo].[Users]
   SET 
   --[Name] = <Name, nchar(100),>
   --   ,[Phone] = <Phone, varchar(100),>
   --   ,[Email] = <Email, varchar(100),>
   --   ,[Password] = <Password, nchar(10),>
   --   ,[Image] = <Image, image,>
	  [RoleId] = 2
 WHERE <Search Conditions,,>


 select top (1) Users.Id from Users inner join Roles on Roles.Id = Users.RoleId where Roles.Name = 'Administrator'


