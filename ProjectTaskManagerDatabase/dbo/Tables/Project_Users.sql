CREATE TABLE [dbo].[Project_Users]
(
	project_id INT FOREIGN KEY REFERENCES Projects(project_id) NOT NULL,
    user_id INT FOREIGN KEY REFERENCES Users(user_id) NOT NULL,
    PRIMARY KEY (project_id, user_id)
)
