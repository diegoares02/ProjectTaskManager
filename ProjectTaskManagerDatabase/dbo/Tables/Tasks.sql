CREATE TABLE [dbo].[Tasks]
(
	    task_id INT PRIMARY KEY IDENTITY(1,1),
    project_id INT FOREIGN KEY REFERENCES Projects(project_id) NOT NULL,
    title VARCHAR(255) NOT NULL,
    description TEXT,
    due_date DATE,
    priority VARCHAR(10) CHECK (priority IN ('Low', 'Medium', 'High')),
    status VARCHAR(15) CHECK (status IN ('To Do', 'In Progress', 'Completed')),
    assigned_to INT FOREIGN KEY REFERENCES Users(user_id),
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE()
)
