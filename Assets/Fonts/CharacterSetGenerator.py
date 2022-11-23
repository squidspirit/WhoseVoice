st = set()
with open("UIText.txt", "r", encoding= "utf-8") as file:
    for line in file.readlines():
        for ch in line:
            if ord(ch) > 1000:
                st.add(ord(ch))

with open("../Resources/Data/Data.json", "r", encoding= "utf-8") as file:
    for line in file.readlines():
        for ch in line:
            if ord(ch) > 1000:
                st.add(ord(ch))

with open("CharacterSet.txt", "w", encoding= "utf-8") as file:
    count = 0
    for i in range(33, 127):
        if (count == 16):
            count = 0
            file.write("\n")
        file.write(chr(i))
        count += 1
    file.write("\n")
    count = 0
    for ch_unicode in st:
        if (count == 16):
            count = 0
            file.write("\n")
            print()
        file.write(chr(ch_unicode))
        count += 1
    file.write("\n")
