# Motivation — Speaker Notes

I've seen a lot of great talks on how LLMs change the practice of coding. Our expectations of the software development lifecycle have been upended. These days, I only open an IDE to debug (and even then, only for pre-LLM code). Code review focuses on proof: it's cheap and easy to spin up a dashboard or demo showing the code does what it promises to do. I let the LLM check for edge cases rather than pore over the code myself.

What's missing is a recognition that our customers' expectations of software have also changed. Large language models are teaching them incredible new ways to interact with software through text, making our beautiful progressive web apps thoroughly obsolete. We need an equal focus on how to deliver applications that meet users where they are, and we need to make sure we actually add value. Large language models can do a lot out of the box!

The good news (for us) is this: our (non-technical) customers are really bad at using LLMs. They will upload entire spreadsheets into context and expect ChatGPT to perform a decent data analysis (to our enterprise customers, ChatGPT is synonymous with LLMs...). They have poor practice when it comes to passwords, security tokens, and sensitive data. And god forbid they start sharing their vibe-coded python apps with each other (sorry not sorry).

The slightly worse news is: LLMs are bleeding edge, and the tools for secure, structured interaction with LLMs are even more bleeding edge. The most popular standard is Model Context Protocol (MCP), and that was *introduced* by Anthropic 18 months ago. MCP formalizes server-client interaction with LLMs, allowing us to deliver software to our customers through chat.

I had a conversation with a senior front-end engineer about LLMs. His take was that the tooling around chat interfaces was not ready for prime time. He thought we should hold off until the ecosystem was a bit more mature. He also struggled with the changing mindset around streaming chat interactions versus a standard REST data contract.

I think this is a pretty widespread idea--web app design is a solved problem! All we need to do is pull in the right node packages and we are done! Incorporating AI into our products is too risky right now!

My opinion is this: we need to start building now, or our customers will leave us behind. The only way to overcome this risk is practice: work with the technology in a low risk environment, with a clearly defined scope. Hence: Hack-a-thon. Scratch that: Vibe-a-thon.
