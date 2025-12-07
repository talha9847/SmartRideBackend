CREATE TABLE Users (
    user_id SERIAL PRIMARY KEY,
    username VARCHAR(50) NOT NULL UNIQUE,
    email VARCHAR(200) UNIQUE,
    mobile VARCHAR(20),
    password_hash TEXT NOT NULL,
    role VARCHAR(20) NOT NULL DEFAULT 'User',
    bio TEXT,
    profile_image_url TEXT,
    cover_image_url TEXT,
    is_verified BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT now(),
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT now()
);                                                