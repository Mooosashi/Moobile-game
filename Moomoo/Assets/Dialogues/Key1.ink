INCLUDE _globals.ink

#speaker: Strange key
#typing sound: Regular
A weirdly shaped key lies still on the grass.

{talkedToBox1:
    -> seenBox1
  - else:
    -> notSeenBox1
}


=== seenBox1 ===
It looks about the size of the hole in the front of that steel crate.
Maybe this is what you need to open it all along.
-> whatToDo


=== notSeenBox1 ===
The function of a key is to open things.
You wonder what lock does this key open.
-> whatToDo


=== whatToDo ===
What will you do?
    * [Pick it up]
        You pick up the key and put it in your inventory. #function: AddItem
        ~ hasKey1 = true
        -> DONE
        
    * [Leave]
        You leave this key behind for someone else to pick it up.
        -> DONE