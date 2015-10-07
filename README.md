#meetapp-mobile
This repository contains the code for the mobile application that I code-named 'meetapp'. The idea was an app that would let users post get-togethers or 'meets' with specific criteria on on who would be able to join such as group size, attendee age and gender. In turn, other users would be able to search for meets in their local area where they satisfied attendee criteria. In short, it's an app to find some people to go biking or rock climbing with.

#Technology
The mobile application is built using Xamarin and MvvmCross. The majority of business logic and client-server interaction is handled in portable c# code. An Android project exists as a proof of concept. The back-end is written in Node.JS and can be found at https://github.com/arutnik/meetapp-server.

Other Technologies used:
1. https://github.com/Fody/PropertyChanged for ViewModels with automatic property changed events.
2. https://github.com/JamesNK/Newtonsoft.Json For JSON deserialization into model classes.
3. https://github.com/tmenier/Flurl Smart HTTP request handling that works in a Xamarin IOS/Android Portable Class Library
4. Xamarin.Facebook (Nuget package) - Facebook API for Xamarin Android

#Tools Required To Build/Run
These tools and subscriptions will be needed to build the application:
1. Visual Studio 2013+ OR Xamarin Studio
2. Xamarin subscription containing Xamarin.Android

#Project Layout

PMA.Mobile.Core/
-Contains the cross platform code in a PCL

PMA.Mobile.Core/Models
-Contains model classes that would be passed around the application.

PMA.Mobile.Core/Models/Servers
-Classes that represent raw server return values, may be automatically deserialized from JSON.

PMA.Mobile.Core/ViewModels
-A ViewModel for binding data and behavior to an Android or IOS View. ViewModels are just an abstraction of the view and contain little logic.

PMA.Mobile.Core/Controllers
-In a MVVMC application the controller is attached to the view model and contains all the logic to populate data and implement UI interactions. This pattern is facilitated by https://github.com/arutnik/mvvmcross-controllers 

PMA.Mobile.Core/Services
-Contains single-responsibility services implemented in pure portable code. Dependencies are injected.

PMA.Mobile.Droid
-A Xamarin Android project that references the PCL and contains Android Views, and Android service implementations.

#Building and running the application

The Android project should compile without any special steps. Some steps will be needed to be able to test social log in (The main feature that is implemented right now)

1. Edit src\PMA.Mobile.Droid\Resources\values\Strings.xml and change the Facebook app id to your own Facebook app id.
2. Edit src\PMA.Mobile.Core\Services\Servers\PmaAppServerServiceUtility.cs GetBaseUrl() to point at a different server if you host the app server somewhere else.
