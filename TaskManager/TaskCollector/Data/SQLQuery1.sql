DROP TABLE TASK
DROP TABLE CUSTOMER

CREATE TABLE CUSTOMER (
Id int not null identity primary key,
Name nVarChar(150) not null,
Phone nVarChar(20) not null,
Mail nVarChar(75) unique,
Company nVarChar(50),
Address nVarChar(150)
)

GO
 
CREATE TABLE TASK (
Id int not null identity primary key,
Description nVarChar(max) not null,
Category nVarChar(20) not null,
Status nVarChar(20) not null,
CustomerId int not null references CUSTOMER (Id)
)

DECLARE @CustomerName nVarChar(150) SET @CustomerName = 'Bo Simmons'
DECLARE @CustomerPhone nVarChar(20) SET @CustomerPhone = '46701111111'
DECLARE @CustomerMail nVarChar(150) SET @CustomerMail = 'mailBo@domain.com'
DECLARE @CustomerCompany nVarChar(50) SET @CustomerCompany = 'Bolaget AB'
DECLARE @CustomerAddress nVarChar(150) SET @CustomerAddress = 'Systemg 40, 123 45 Svingelsta'
IF NOT EXISTS (SELECT Id FROM CUSTOMER WHERE Name = @CustomerName) INSERT INTO CUSTOMER (Name, Phone, Mail, Company, Address) VALUES  (@CustomerName , @CustomerPhone, @CustomerMail , @CustomerCompany , @CustomerAddress )

DECLARE @TaskStatus nVarChar(20) SET @TaskStatus = 'Paused'
DECLARE @TaskCustomerId nVarChar(20) SET @TaskCustomerId = '1'
DECLARE @TaskCategory nVarChar(20) SET @TaskCategory = 'Client'
DECLARE @TaskDescription nVarChar(max) SET @TaskDescription = 'Beskrivning av ärendet'
IF NOT EXISTS (SELECT CustomerId, Category FROM TASK WHERE Id = @TaskCustomerId AND Category = @TaskCategory) INSERT INTO TASK (Description, Category, Status, CustomerId) VALUES (@TaskDescription , @TaskCategory , @TaskStatus , @TaskCustomerId)

SELECT * from CUSTOMER
SELECT * from  TASK
