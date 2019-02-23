# Notes Finder

Given a set of notes and a target value, the class tries to retrieve the right notes to avoid change, when not possible it returns null.

# Running

As I've written it for a test, the sample application expects two paths as arguments (in and out):

Example of initial data file (in.txt), where the first value is the target value and the remaining the available notes:
```
100;100;
100;50;70;120;150
12;1;9;7;3;5;
30;1;2
30;60;15;9;9;9;8;8;5;5;3;3;3;3;3;2;2;2;1
2147483647;2147483500;100;40;5;1;1;1;1
100;100;
100;50;70;120;150
12;1;9;7;3;5;
30;1;2
30;60;15;9;9;9;8;8;5;5;3;3;3;3;3;2;2;2;1
2147483647;2147483500;100;40;5;1;1;1;1
100;100;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;1;
```

Example of output file (out.txt):
```
100
NO
5;7
NO
15;9;5;1
2147483500;100;40;5;1;1
100
```

# Missing

I was going to add an extra cache to avoid checking the same paths. But didn't! =]