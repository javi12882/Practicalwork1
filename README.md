# Practicalwork1

# Practical Work 1 
This is the repository of the practical work 1 in C#. 

# INDEX

- [Introduction](#introduction)
- [Description](#docking-simulator-software-practical-work-i)
- [Problems](#problems)
- [Conclusions](#conclusions)


=======
# Introduction

The propose of this practical work I is to develop a simulator of the trains and the platforms of the station to make them docked in the corresponding platform based on the disponibility of them. All the work is based on C# on Object Oriented Programming and with methods such as polymorphism or inheritance, encapsulation or abstraction.

This project must contain a tick-based system and after every tick, the program must update in order to show new information about the trains arrival time at that moment and also the station Status showing if there is any free platform for docking the trains. 
Every train can be in 4 different states:
    · EnRoute (when the program starts)
    · Waiting (when the train has reached 0 minutes of arrival time and waiting for a free platform)
    · Docking (when the platform is free for docking)
    · Docked (when the train has already docked in a free platform)

There are 2 different types of trains:
    · Passenger train (with number of carriages and capacity)
    · Freight train (with maximum weight and the type of cargo being transported)
    

When running the program, it must execute a menu with a title welcoming the user to the station. After that, you have to choose if you want to insert a pre-made document with different trains or select the second option which is making new trains for the program. After that you can start your simulation manually of automatically with a tick-based system.


# Description 

The propose of this practical work I is to develop a simulator of the trains and the station platforms to make them dock in the corresponding platform based on the disponibility of them. All the work is based on C# on Object Oriented Programming and with methods such as polymorphism or inheritance.
We decided to create a clear menu and easy understandable for the user.
The interaction of the user is really simple as it has to choose firstly if it wants to load a file that is already created or if it wants to create all the new trains by its own. After that if he chooses using the simulation manually, it will print a text saying that for doing the different "ticks" in order to update the program you would have to press enter and it will change the different status of the trains that are running in that moment. If the user choose the automatic simulation, every 2.5 seconds the program will be updated automatically, changing the status of the trains and the platforms of the station.

# Problems 

At first I thought the practical work would be just doing some different methods, classes and subclasses for the train class but as I was doing it I realized it was not that easy and that we would have to stay a lot of hours developing the program, testing it, finding errors, fixing them... 

I faced some problems during the program construction as I had to revise the slides of the different units to revise all the theory.
Mainly, we had most of the problems in the station class as it is the largest one and the one that contains most of the methods of the program.
We had some problems trying to set which was the status of the trains in the different moments of the simulation. 
To continue, our trains, during all the program execution, stayed always in the "En route" state even if the distance was 0 but in the end we managed to solve it.


# Conclusions

To conclude, this project has made me realize that it is really complicated to develop a real full program although this, compared to other different fully developed programs it could be like just a "warm up". I understood how much time could a full team last to make a real program for different daily life situations.

With this practical work 1 I learned how to made the different relationships for the different subclasses as well as finding how to load a pre-made file with information of different aircrafts. 






>>>>>>> 0d8aaa5506521d988476090521c23dc41c3798fc