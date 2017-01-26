Attributes
===
Active attribute (ex: Health) implementation for Unity3D, inspired by SoulsBourne.

Overview
---
Attribute are implemented with Reactive Property in mind, in which case the value are a FloatReactiveAttribute and clamped between 0 to MaxValue. Value are changed by function, and it can be read using reactive subscriber.
```
// Example for checking health
_AttributeHealth.Value.Where(x => x==0).Subscribe(x => 
{
    // Handle dead
});

// Will decrease current value and clamp it to 0 if needed
_AttributeHealth.DecreaseValue(10);
```

Dependencies
---
* [Unity 5.5.0f3](https://unity3d.com/unity/whats-new/unity-5.5.0)
* [UniRx 5.5](https://github.com/neuecc/UniRx)


How to Use
---
Attach Attribute component to a GameObject and assign it's ID. Any component who needs the attribute will need to get it's reference first before it can be used.
```
Attribute[] attributes = _AttributeOwner.GetComponents<Attribute>();
_AttributeStamina = attributes.Where(x => x.ID.Equals("ATTRIBUTE_STAMINA")).FirstOrDefault();
```
There are currently three types of Attribute Component:
* Attribute: For basic things that only need to clamped Increase and Decrease.
* AttributeRegenerative: Attribute that will regen back everytime it decreases.
* AttributeOverTime: Attribute that won't directly change values, but do it over time with certain rate.

After assigning the Attribute, initialize the value on the component accordingly. MaxValue will always need to be initialized (ex: any value > 0), and if you use other Attribute types, there are few variables that can be tweaked (ex: delay, regen rate on AttributeRegenerative).

Example Usage
---
There are a few example implementation on ExampleScene which are mostly based on Dark Souls and Bloodborne mechanics:

* Stamina can be implemented using basic AttributeRegenerative. It will handle the delay before regeneration differently if it's 0 or above 0.

![Stamina](http://i.imgur.com/6jlq8as.gif)

* Dark Souls style health are simply two Attribute components (one base and one over time) which are decreased on the same time. 

![DS Health](http://i.imgur.com/8J8NFvv.gif)

* Bloodborne health absorb mechanic are combined by changing the over time destination value on the AttributeOverTime. 

![BB Health](http://i.imgur.com/O9rEbLN.gif)

Games that are using [Attribute](https://github.com/hitnoodle/attributes)
---
* [Maze Shroom VR](https://play.google.com/store/apps/details?id=com.tinker.perangkakvr&hl=in)
* [Hollow Rhyme](https://steamcommunity.com/sharedfiles/filedetails/?id=828109436)
* [Cave Blind](http://globalgamejam.org/2017/games/cave-blind)
