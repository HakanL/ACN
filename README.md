This project was originally developed on [CodePlex](http://acn.codeplex.com). 
------

#Architecture for Control Networks (ACN)

### Project Description
Architecture for Control Networks (ACN) is a network control protocol which is used in the entertainment industry. 

This open source project aims to provide a full implementation of this standard and many of the sub protocols associated with ACN.

### Introduction
The ACN protocol is a suite of standards being developed by the lighting industry for control of lighting fixtures and other devices used by the entertainment industry. The first release of the protocol is ANSI E1.17 and other stndards have been released which form part of the suite. You can find out more about ACN on [wikipedia](http://en.wikipedia.org/wiki/Architecture_for_Control_Networks) or on the [PLASA Technical Standards](http://www.plasa.org/standards) website.

This implementation of ACN is intended to be used by projects which wish to add ACN capability to their products. The code is written in C# and fully managed.

## Supported Standards

This project is under continuous development and further protocols will be added. Here is a list of the current support provided by this library.

| Protocol Name                                          | Standard                       | Implementation Status      |
| -------------------------------------------------------|:------------------------------ |:--------------------------:|
| Root Layer Protocol (RLP)                              | ANSI E1.17                     | Complete                   |
| Session Data Transport Protocol (SDT)                  | ANSI E1.17                     | Not Implemented            |
| Service Location Protocol (SLP)                        | RFC 2609                       | Complete                   |
| Simple Network Time Protocol (SNTP)                    | RFC 2030 ANSI E1.30-3 - 2009   | Complete                   |
| Trivial File Transfer Protocol (TFTP)                  | RFC 1350                       | Not Implemented            |
| Device Description Language (DDL)                      | ANSI E1.17                     | Not Implemented            |
| Device Management Protocol (DMP)                       | ANSI E1.17                     | Partial                    |
| Streaming ACN (sACN)                                   | ANSI E1.31                     | Complete                   |
| RDM Extension (RDMNet)                                 | ANSI E1.33                     | Complete                   |
| Remote Device Management (RDM)                         | ANSI E1.20                     | Complete                   |

## Contribute
The original project remains in active development ([as of October 2015](http://acn.codeplex.com/SourceControl/list/changesets)). You may wish to contribute there instead of on this project.

## Credits
| Known Name                                                                | Role                           |
| --------------------------------------------------------------------------|:------------------------------ |
| [owaits](http://www.codeplex.com/site/users/view/owaits)                  | CodePlex project coordinator   |
| [BetacomPhoenix](http://www.codeplex.com/site/users/view/BetacomPhoenix)  | CodePlex project developer     |
| [Gregory Haynes](http://www.codeplex.com/site/users/view/gregoryhaynes)   | CodePlex project developer     |
| [Mark Daniel](http://www.codeplex.com/site/users/view/MarkDaniel)         | CodePlex project developer     |
| [Mike James](https://github.com/MikeCodesDotNet)                          | ArtNet Receive sample developer|
| [Hakan Lindestaf](https://github.com/HakanL)                              | GitHub Maintainer / Forker     |
