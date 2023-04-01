# **Contents**
Use these hyperlinks to quickly navigate through the different sections of this readme.
- [About](#about)  
- [Folders](#folders)
    - [Components](#components)
    - [Interfaces](#interfaces)
    - [Resources](#resources)
- [Maintenance](#maintenance)

# **About**
OverflowSim is a model which can be used to simulate dike overflows. It consists of the necessary tools to calculate at which point in time a dike of a certain height will overflow in a specific scenario. The simulation model was originally developed to contribute to the research on dike overflows for the _Research Methods_ course at _Utrecht University_. The application is created using C# (.NET6) and WinForms.   

![OverflowSim-logo](https://pbs.twimg.com/media/Fso9r_jWAAIxsDV?format=jpg&name=medium)

# **Folders**
```
DikeOverflowModel
├───Components
├───Interfaces
└───Resources
```
## **Components**
Contains all classes related to the different components in the editor. 
- LogView.cs
- OverflowGraph.cs
- SettingsView.cs
- SimulationRenderer.cs
- SimulationView.cs

## **Interfaces**
Contains all interfaces implemented by specific components. Mainly used for the implementation of the _observer pattern_.
- IObservable.cs
- IObserver.cs

## **Resources**
Containing all images used by the application. 

# **Maintenance**
As this application was created specifically for the _Research Methods_ university course, it will no longer be updated.   

