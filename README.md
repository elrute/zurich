This repo contains the answer, in the shape of a microservice in .net with Entity fwk following DDD, to the followig code challenge:

Task description:
=================

  Production line processes following objects: points (a very small objects), circles, rectangles and squares that have certain position and size.

  1. Design a text file format to collect the objects from the production line
     - create a example of the text with all object types

  2. Design SQL database structure to store the data
     - If you have a SQL DB available locally, create the database. If not, describe the database structure in a text file.
     - use data access components of your choice

  3. Implement code that:
    a. reads the data objects from the database 
       - if real database is not available, implement a code that would pretend reading the data.

    b. write code that displays the stored objects in their original positions by calling the Screen class below (do not implement the methods inside of Screen class)

      class Screen
      {
         DrawLine(int x1, int y1, int x2, int y2);
         DrawCircle(int x, int y, int radius);
         DrawPoint(int x, int y);
      }

    c. write code that moves all the objects in provided direction x,y provided as parameters

  4. List test cases to automate the functionality
     - code one test for one test case of your choice (skeleton is fine)

  Note: If you would find multiple ways how to interpret the business requirements write them all down and explain why you chose the one that you implement.

  Compress the .net code as single zip file and shared it based on provided instructions.
  

  Happy coding :)
