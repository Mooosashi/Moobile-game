INCLUDE _globals.ink

{ talkedToBox1:
- 0: -> main
- 1: -> time1
- else: -> time1
}

=== main ===
#speaker: Steel crate
#typing sound: Regular
A lifeless steel crate, sitting alone in the middle of nowhere.
It has to feel so lonely. You decide to call it "The loneliest steel crate ever".
"Wow! What a cool name!" #speaker: The loneliest steel crate ever #typing sound: Voice1
Wait, did this steel crate just talk?  #typing sound: Regular
Some kind of energy is powering it but there are no cables around. Must be run by batteries. 
You wonder about all the things that could be stored inside.
~ talkedToBox1 = 1
-> whatToDo


=== whatToDo ===
What will you do?

{hasKey1:
    * [Kick it]
        A metallic thud resonates. Nothing happens.
        What will you do now?
        
            * * [Kick it again]
                The strike sounds louder. But the box is still wide shut.
                You start to think there may be another way of getting it  to open.
                -> whatToDo
                
            * * [Apologize]
                You apologize with vigor to the inanimate object. To open this crate you will need more than just kind words.
                -> whatToDo
                
    * [Open it]
        The key gently slides into the hole.
        A metallic click escapes from the mechanism.
        A little handle pops up on the side of the structure as it invited you to look inside.
        -> DONE
    
    * [Leave]
        Maybe some type of <color=\#CB7715>key</color> will do it.
        For now, you leave this container to it's own fate.
        -> DONE
  - else:
    * [Kick it]
        A metallic thud resonates. Nothing happens.
        What will you do now?
        
            * * [Kick it again]
                The strike sounds louder. But the box is still wide shut.
                You start to think there may be another way of getting it  to open.
                -> whatToDo
                
            * * [Apologize]
                You apologize with vigor to the inanimate object. To open this crate you will need more than just kind words.
                -> whatToDo
                
    
    * [Leave]
        Maybe some type of <color=\#CB7715>key</color> will do it.
        For now, you leave this container to it's own fate.
        -> DONE
}


=== time1 ===
#speaker: The loneliest steel crate ever
#sound: Regular
Just as lonely as you remember.
It's shape, texture and look. Indeed, it is the same box you left a while ago.
-> whatToDo