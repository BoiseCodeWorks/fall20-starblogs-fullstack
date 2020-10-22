-- CREATE TABLE profiles (
--   id VARCHAR(255) NOT NULL,
--   name VARCHAR(255) NOT NULL,
--   email VARCHAR(255) NOT NULL,
--   picture VARCHAR(255) NOT NULL,
--   PRIMARY KEY(email)
-- )

CREATE TABLE blogs(
  id int AUTO_INCREMENT,
  name VARCHAR(255) NOT NULL,
  description VARCHAR(255) NOT NULL,
  img VARCHAR(255),
  published TINYINT,
  creatorEmail VARCHAR(255) NOT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (creatorEmail)
    REFERENCES profiles(email)
    ON DELETE CASCADE
)