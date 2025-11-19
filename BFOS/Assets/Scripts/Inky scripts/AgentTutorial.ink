VAR cameraPos = ""
VAR gobloombaSpawn = false
VAR nextScene = false

(???) - Hey You!

(You) - ...

~ cameraPos = "AgentIn"

(???) - Is that a Big Fuck Off Sword in your hammerspace, or are you just happy to see me?

~ cameraPos = "AgentToPlayer"

(You) - Who are you and why are you hitting on me?

~ cameraPos = "PlayerToAgent"

(???) - Apologise citizen, allow me to introduce myself. 
(???) - I am a Federal Agent of the kingdom, sent to help the keeper of the Big Fuck Off Sword, which appears to be yourself.
(Agent Tutorial) - Name's Tutorial, Pleasure to meet you.

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
(Agent Tutorial) - Do you even know the power of...
(Agent Tutorial) - The.
(Agent Tutorial) - Big.
(Agent Tutorial) - Fuck.
(Agent Tutorial) - Off.
(Agent Tutorial) - SWORD!?!

~ cameraPos = "AgentToPlayer"

(You) - Not particularly, I've only just booted up the game so nothing has been properly explained to me yet.

~ cameraPos = "PlayerToAgent"
~ gobloombaSpawn = true

(Agent Tutorial) - Great. With everything in the balance and you don't even-

~ cameraPos = "AgentOut"

(Gobloomba) - GET BACK 'ERE 'FORE I TURN YOU INTA SOME SORTA VAUGELY BRI-ISH DISH!!!

~ cameraPos = "AgentIn"

(Agent Tutorial) - Oh this fucking guy...

(Agent Tutorial) - Why are Goblins always British?

(Agent Tutorial) - Alright Just follow my lead, and try not to get yourself killed.

~ cameraPos = "AgentOut"

(You) - Okay...

~ nextScene = true

->DONE


