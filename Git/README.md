# GitHub Change Commit Author

Set Notepad as default git text editor
```
git config core.editor notepad
```
Start rewriting history like changing author or delete a commit.
```
git rebase -i -p <commit>
```

Notepad opens with a list of commits. Overwrite "pick" with "edit" for the commit you want to change the author.

```
git commit --amend --author "Author Name <email>"
git rebase --continue
git push -f origin master
```