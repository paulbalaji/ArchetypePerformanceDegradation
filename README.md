# Archetype performance degradation repro repo

Kudos: [samiwh](https://github.com/samiwh)

1. clone this repo

2. open this project with Unity

3. build out and run

4. observe FPS drop over time

#### Our results

* Unity 2018.2.14f1
* Intel(R) Xeon(R) W-2195 CPU @ 2.30GHz, 2304 Mhz, 18 Core(s), 36 Logical Processor(s)
* 64 GB RAM
* GTX 1080
* Built out player windowed
* 640x480 resolution
* `Very low` graphics

![](results.png)

[See raw results here](results.txt)

#### What is happening?

Every 30 seconds, we add and remove an entity 50,000 times. You should see performance degrade as the number of unique component groups increases over time.

> Check out the [`all_apis`](https://github.com/paulbalaji/ArchetypePerformanceDegradation/tree/all_apis) branch if you want to see a comparison of different ECS APIs.
