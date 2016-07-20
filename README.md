# SearchAThing.Patterns

Software patterns

## Build

There are many dependencies between projects in relative path directly from other source repositories,
in order to build successfully its suggested to clone follow repository [SearchAThing](https://github.com/devel0/SearchAThing) containing all of them.

## GUI

### UITask

Task and GUI Dispatcher annotations ( see [here](https://searchathing.com/?p=1470) ).

[Code](https://github.com/devel0/SearchAThing.Patterns/blob/master/src/StatusBarTaskDispatcher/MainWindow.xaml.cs)

### StatusBarTaskDispatcher

Update a statusbar using Task.

[Code](https://github.com/devel0/SearchAThing.Patterns/blob/master/src/StatusBarTaskDispatcher/MainWindow.xaml.cs)

## Mongo DB

### MongoDBWpf

Mongodb GUI WPF mappings.

[Code](https://github.com/devel0/SearchAThing.Patterns/blob/master/src/MongoDBWpf/MainWindow.xaml.cs)

### MongoScopedChildren

Access the root document from the nested child objects.

[Code](https://github.com/devel0/SearchAThing.Patterns/blob/master/src/MongoScopedChildren/Program.cs)

### MongoConcurrency

Handles in a semi-automatic way update of only changed fields.

[Example](https://github.com/devel0/SearchAThing.Patterns/tree/master/src/MongoConcurrency)

## C++ Interfacing

In this [example](https://github.com/devel0/SearchAThing.Patterns/tree/master/src/CPPClassInterfacing) is shown how to call C++ methods from a managed [library](https://github.com/devel0/SearchAThing.Patterns/tree/master/src/CPPClassTest) ( just add the /clr flag to the C++ compilation ).