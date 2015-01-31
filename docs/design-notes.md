# Design Notes of CodeSharper

Basic design notes in **CodeSharper**. **CodeSharper** is an **extensible refactoring tool**.

## TextDocument design notes

TextDocument is basically a pool for text ranges. It represents the whole edited text document (or source code in most cases) in CodeSharper. It stores **text ranges** for editing sections in source code. We can imagine text documents as an *object pool of text ranges*.  


## TextRange design notes

Text ranges are the essential data types in CodeSharper. They represent a higher abstraction layer of text documents and source codes as well. TextRange contains `[start, stop]` informations beside `text` value. 

![](graphics/text-ranges.png)

### TextRange features

- **remove selected text range** from text document (source)
- **update/replace text** of range in text document (source)
- **attach to/detach from** (like a node in *HTML*) text document

## Components of CodeSharper

TODO