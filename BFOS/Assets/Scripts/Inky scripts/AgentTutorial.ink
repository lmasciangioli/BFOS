VAR cameraPos = ""

(???) - Hey You! (Press P to Continue Conversation)

(You) - ...

~ cameraPos = "AgentIn"

(???) - Is that a Big Fuck Off Sword in your hammerspace, or are you just happy to see me?

~ cameraPos = "AgentToPlayer"

(You) - Who are you and why are you hitting on me?

~ cameraPos = "PlayerToAgent"

(???) - Apologise citizen, allow me to introduce myself. 
(???) - I am a Federal Agent of the kingdom, sent to help the keeper of the Big Fuck Off Sword, which appears to be yourself.
(Agent Tutorial) - Name's Tuturial, Pleasure to meet you.

(Agent Tutorial) - As for why I am hitting on you, 

(Agent Tutorial) - it is due to a deep seated loneliness that materialises itself in making inappropriate comments towards others.

(Agent Tutorial) - You see my father was a drunk and...

~ cameraPos = "AgentToPlayer"

(You) - ...

~ cameraPos = "PlayerToAgent"

(Agent Tutorial) - ...
(Agent Tutorial) - In anycase, as I mentioned, I'm here to help

~ cameraPos = "AgentToPlayer"

*[Why are you just a rectangle?] <>
-> next
*[Why do I keep moving left, but the camp around me stays the same when I reach a door?] <> 
-> next

==next==
~ cameraPos = "PlayerToAgent"
(Agent Tutorial) - What an excellent question
(Agent Tutorial) - Anyways
(Agent Tutorial) - I've been watching you from afar, and left those handy signs pointing you to my direction.
(Agent Tutorial) - You have a knack for avoiding these guys, but do you know how to truely defeat them?
->DONE


