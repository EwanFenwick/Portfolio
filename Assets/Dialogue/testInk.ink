-> main

=== main ===
Behold! I am speaking to you now!
    + [Wow!]
        -> chosen("Wow")
    + [Dialogue is awesome!]
        -> chosen("Awesome")
    + [Kinda boring tbh]
        -> rude
    
    
=== chosen(text) ===
That's right! {text} indeed!
->END

=== rude ===
Sorry. It's all I have.
    +[I'm sorry. That was rude of me.]
        That's okay.
        -> END
    +[...]
        I wonder what would happen if I fell off this plane.
        -> END
    +[*Shrug*]
        Yeah.
        -> END