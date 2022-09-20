# TurnBasedRPG

A prototype of a JRPG style game I was working on a few years back.
Spent most of my effort designing the event flow for turn tracking.

> ## 2022 Commentary
> 
> Some of the details of this project are lost to history, but looking back
> the lesson I still remember learning from this project was the division
> between high-level "game logic" and systems programming. I wasted a lot of
> time trying to apply fool-proof API design principles to the turn-based
> logic. Particularly with how a "turn end" was communicated from the actors
> back to the loop management logic.
>
> The waste wasn't in trying to make a nice, clean API; it was a low-stakes
> learning project, afterall. The waste was that I had unrealistic
> expectations about how high those standards should be with something as
> flexible as a game. Yes, it would be nice to have a turn-taking system
> that was guaranteed to never get stuck (as many of us have no doubt
> experienced as players) but unlike, say, a trade messaging system there
> is no clear line for what constitutes a full "turn." Characters don't
> just pick actions, apply stat changes, and move on: sounds and animations
> play, and the game designers might even want to place whole mini-games
> into a turn (ala Tifa's Slot Machine Limit Break, or the action inputs in
> Paper Mario).
>
> **So the implementation takeaway here** that I came to was that the turn
> system shouldn't dictate what a turn is, but rather provide the "turn
> taking" systems the tools to cleanly end their turn and return control.
> Things like callbacks and event loops I think would be good solutions.
