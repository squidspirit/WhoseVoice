use whosevoice;

-- insert into cvs (`name`) values ('佐倉綾音');
-- insert into animes (`title`) values ('請問您今天要來點兔子嗎？');
-- insert into characters (`anime_id`, `id`, `name`, `cv_id`) values (1, 1, '保登心愛', 1);
-- insert into clips (`anime_id`, `character_id`) values (1, 1);

-- Full sheet
select
    `clips`.`id`,
    `animes`.`id` as `anime_id`,
    `animes`.`title` as `anime`,
    `characters`.`id` as `character_id`,
    `characters`.`name` as `character`,
    `cvs`.`id` as `cv_id`,
    `cvs`.`name` as `cv`
from `clips`
join `animes` on `animes`.`id` = `clips`.`anime_id`
join
    `characters`
    on `characters`.`anime_id` = `clips`.`anime_id`
    and `characters`.`id` = `clips`.`character_id`
join `cvs` on `cvs`.`id` = `characters`.`cv_id`
order by `clips`.`id`;


-- Full json
select 
  json_object(
    'animes', `_animes`,
    'cvs', `_cvs`, 
    'clips', `_clips`
  ) as `_result` 
from 
  (
    select 
      json_arrayagg(
        json_object(
          'id', `id`,
          'name', `name`
        )
      ) as `_cvs` 
    from 
      `cvs`
  ) as `_cvs`, 
  (
    select 
      json_arrayagg(
        json_object(
          'id', `id`,
          'anime', `anime_id`,
          'character', `character_id`
        )
      ) as `_clips` 
    from 
      `clips`
  ) as `_clips`, 
  (
    select 
      json_arrayagg(
        json_object(
          'id', `anime_id`,
          'title', `title`,
          'characters', `_characters`
        )
      ) as `_animes` 
    from 
      (
        select 
          `c`.`anime_id`, 
          `a`.`title`, 
          json_arrayagg(
            json_object(
              'id', `c`.`id`,
              'name', `name`,
              'cv', `cv_id`
            )
          ) as `_characters` 
        from 
          `animes` as `a` 
          join `characters` as `c` on `c`.`anime_id` = `a`.`id` 
        group by 
          `anime_id`
      ) as `a`
  ) as `_animes`;


-- CVs json
select 
  json_object(
    'cvs', 
    json_arrayagg(
      json_object('id', `id`, 'name', `name`)
    )
  ) as `_result` 
from 
  `cvs`;


-- Clips json
select 
  json_object(
    'clips', 
    json_arrayagg(
      json_object(
        'id', `id`,
        'anime', `anime_id`,
        'character', `character_id`
      )
    )
  ) as `_result` 
from 
  `clips`;


-- Animes json
select 
  json_object(
    'animes', 
    json_arrayagg(
      json_object(
        'id', `anime_id`,
        'title', `title`, 
        'characters', `_characters`
      )
    )
  ) as `_result` 
from 
  (
    select 
      `c`.`anime_id`, 
      `a`.`title`, 
      json_arrayagg(
        json_object(
          'id', `c`.`id`,
          'name', `name`,
          'cv', `cv_id`
        )
      ) as `_characters` 
    from 
      `animes` as `a` 
      join `characters` as `c` on `c`.`anime_id` = `a`.`id` 
    group by 
      `anime_id`
  ) as `a`;
