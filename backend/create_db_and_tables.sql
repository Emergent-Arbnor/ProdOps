-- Create the database if it doesn't exist
SELECT 'CREATE DATABASE prodops'
WHERE NOT EXISTS (SELECT 1 FROM pg_database WHERE datname = 'prodops');\gexec

-- Connect to the prodops database
\c prodops;

-- Create the users table if it doesn't exist
CREATE TABLE IF NOT EXISTS users (
    id SERIAL PRIMARY KEY,
    username VARCHAR(10) UNIQUE NOT NULL CHECK (LENGTH(username) >= 5),
    password TEXT NOT NULL,
    isadmin BOOLEAN NOT NULL
);

-- Create the articles table if it doesn't exist
CREATE TABLE IF NOT EXISTS articles (
    id SERIAL PRIMARY KEY,
    article_name VARCHAR(10) UNIQUE NOT NULL CHECK (LENGTH(article_name) >= 5),
    article_count INTEGER NOT NULL CHECK (article_count >= 0)
);

INSERT INTO articles 
    (article_name, article_count)
VALUES 
	('ARTICLE_A', 0),
	('ARTICLE_B', 0);