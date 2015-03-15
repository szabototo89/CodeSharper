parser grammar Markdown;

options { tokenVocab=MarkdownLexer; }

start: header;

header: LIST_MARK TEXT NEW_LINE;