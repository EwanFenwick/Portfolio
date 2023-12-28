-> main

=== main ===
Behold! I am speaking to you now!
    + [Wow!]
        -> chosen("Wow")
    + [I love talking to test NPCs]
        -> sayMore
    + [Kinda boring tbh]
        -> rude
    
    
=== chosen(text) ===
That's right! {text} indeed!
->END
        
=== sayMore ===
To test what nows?
Oh well, no matter.
Cheerio!
-> END

=== rude ===
Sorry. It's all I have.
    +[I'm sorry. That was rude of me.]
        That's okay.
        -> END
    +[...]
        Some conversation partner you are.
        -> END
    +[*Shrug*]
        Yeah.
        -> END