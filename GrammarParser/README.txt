https://github.com/antlr/antlr4/tree/master/runtime/CSharp
https://github.com/antlr/grammars-v4

https://github.com/antlr/antlr4/blob/master/doc/getting-started.md


## Generate code from grammar definition
antlr -Dlanguage=CSharp .\{{grammar}}.g4

## Run TestRig
grun org.antlr.v4.gui.TestRig %*