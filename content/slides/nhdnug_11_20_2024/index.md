---
title: Why is all of my data in Parquet?
layout: layouts/slide
author: Chris Bremer
date: 2024-05-27
tags:

---

<!-- Any section element inside of this page is displayed as a slide -->
<section>

### Data Engineer's (**DE**) office 
<p class="fragment text-italic">
  Chris (<b>CB</b>) enters. It looks like he want to ask a question.
</p>
</section>
<section>
<ul class="dialog">
<li> CB: Hey, I'm trying to run some sanity checks on our data, but I can't find it in Azure SQL.
<li class="fragment"> DE: We migrated everything to the Data Lake.</li>
<li class="fragment">DE: It's mostly in a data format called <strong>Parquet</strong>.</li>
<li class="fragment" > 
     DE: You'll have to rewrite your query in Python. 
</li>
<li class="fragment" > 
     DE: It only works if you run it from a notebook in the browser IDE.
</li>
<li class="fragment"> 
     DE: You can't do anything until you spin up some compute. That should take 5-10 minutes.
</li>
</ul>

</section>

<section>

## Why the #%$@ is all my data in Parquet?
<h4 class="fragment">(A deep dive into the data lake) </h4>

</section>

<section>

## What is a data lake?
<div class="r-stack">
     <div class="fragment fade-out">
     {% image "lake_dragon.jpg", "Lake Conroe dragon", "100vw"
     %}
     </div>
     <table class="fragment">     
       <tr>
          <td>
               <h4> Databricks</h4>
               {% image "databricks.png", "Whiteboard with lecture notes", "50vw"
               %}
          </td>
          <td>
               <h4>Snowflake</h4>
               {% image "databricks.png", "Whiteboard with lecture notes", "50vw" 
               %}
          </td>
     </table>

</div>


</section>
<section>

## Markdown 2

Content 2.1

</section>
<section>

## Markdown 3.1

Content 3.1

</section>

<section>

## Markdown 3.2

## Content 3.2

</section>
<section>

## Markdown 3.3

{% image "databricks.png", "Whiteboard with lecture notes", "25vw, 50vw"
%}


</section>
