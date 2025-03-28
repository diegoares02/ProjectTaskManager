CREATE TABLE [dbo].[Roles]
(
	role_id INT PRIMARY KEY IDENTITY(1,1),
    role_name VARCHAR(50) UNIQUE NOT NULL,
    description TEXT
)
