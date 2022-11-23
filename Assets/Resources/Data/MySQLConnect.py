import mysql.connector
import json

connection = mysql.connector.connect(
    host = "finalproject.ddns.net",
    database = "whosevoice",
    user = "whosevoice",
    password = "whosevoicepwd"
)
cursor = connection.cursor()


cursor.execute("\
    select \
        json_object(\
            'animes', `_animes`,\
            'cvs', `_cvs`, \
            'clips', `_clips`\
        ) as `_result` \
    from \
    (\
        select \
            json_arrayagg(\
                json_object(\
                    'id', `id`,\
                    'name', `name`\
                )\
            ) as `_cvs` \
        from \
            `cvs`\
    ) as `_cvs`, \
    (\
        select \
            json_arrayagg(\
                json_object(\
                    'id', `id`,\
                    'anime', `anime_id`,\
                    'character', `character_id`\
                )\
            ) as `_clips` \
        from \
            `clips`\
    ) as `_clips`, \
    (\
        select \
            json_arrayagg(\
                json_object(\
                    'id', `anime_id`,\
                    'title', `title`,\
                    'characters', `_characters`\
                )\
            ) as `_animes` \
        from \
        (\
            select \
                `c`.`anime_id`, \
                `a`.`title`, \
                json_arrayagg(\
                    json_object(\
                        'id', `c`.`id`,\
                        'name', `name`,\
                        'cv', `cv_id`\
                    )\
                ) as `_characters` \
            from \
                `animes` as `a` \
            join `characters` as `c` on `c`.`anime_id` = `a`.`id` \
            group by \
                `anime_id`\
      ) as `a`\
  ) as `_animes`;\
")


data_json = json.loads(cursor.fetchall()[0][0])
data_json_text = json.dumps(data_json, indent= 4, ensure_ascii= False)
open("Data.json", "w", encoding= "utf8").write(data_json_text)
print(data_json_text)

cursor.execute("\
    select\
        `clips`.`id`,\
        `animes`.`id` as `anime_id`,\
        `animes`.`title` as `anime`,\
        `characters`.`id` as `character_id`,\
        `characters`.`name` as `character`,\
        `cvs`.`id` as `cv_id`,\
        `cvs`.`name` as `cv`\
    from `clips`\
    join `animes` on `animes`.`id` = `clips`.`anime_id`\
    join\
        `characters`\
        on `characters`.`anime_id` = `clips`.`anime_id`\
        and `characters`.`id` = `clips`.`character_id`\
    join `cvs` on `cvs`.`id` = `characters`.`cv_id`\
    order by `clips`.`id`;\
")
full_sheet = cursor.fetchall()
with open("Data.csv", "w", encoding= "utf8") as file:
    file.write("id, anime_id, anime, character_id, character, cv_id, cv\n")
    for tup in full_sheet:
        file.write(str(tup)[1:-1].replace("\'", "") + "\n")