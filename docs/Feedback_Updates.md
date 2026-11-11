# Updates from lecturer feedback

Lecturer feedback on Part 2:

> Outstanding work! The application demonstrates excellent integration of advanced data structures, functional event management and intelligent recommendations. Further improvements should focus on completing navigation features and refining personalised recommendation scoring.

## Navigation

The main menu had three buttons, but **Service Request Status** was disabled and labelled "available later". A submitted issue also ended on the report form. The resident could not open the request they had just created.

What changed:

- `Forms/MainMenuForm.Designer.cs` enables the button and names it **3.  Service Request Status**.
- `Forms/MainMenuForm.cs` opens `ServiceRequestForm`. After every dialog it refreshes two counts: issues reported this session, and open service requests.
- `Forms/ReportIssuesForm.cs` stores the new issue as a service request, then asks **Track this request now?** Yes sets `TrackReference` and closes. The report form does not open the status form itself. The main menu reads `TrackReference` inside the `using` block, after `ShowDialog` returns, and opens the tracker on that same `MSA-` reference.

How to see it: submit a report with location `14 Church Street, Bellville`, choose **Yes**, and the status page opens on that reference with status Submitted.

## Recommendation scoring

The old score treated the all-time favourite category as the only strong signal, and a date match was a flat +2 anywhere inside seven days. An old search could lock the list.

What changed, in `Data/EventCatalogue.cs` and the recommendation card in `Forms/LocalEventsForm.cs`:

- The search queue is the only search signal. `RecordSearch` no longer increments a lifetime counter.
- The last five searches that have a category weigh 5, 4, 3, 2, then 1. The heaviest category scores +4. The second scores +2. A search with no category is skipped, so it cannot become the favourite.
- The newest search that has a date scores +3 on that day, +2 within three days, and +1 within seven days.
- Opening an event adds 1 to that category, capped at 3. `PushRecentlyViewed` writes this capped dictionary.
- An event scores +1 more than an announcement. Priority 3 or higher still scores +2.
- Each card shows `Score n` and the `Why:` line. Events already on the viewed stack stay excluded.

Worked example. Search **Utilities**, search **Utilities** again, then search **Recreation**, with **Any date** left ticked.

- Newest search, Recreation, weight 5.
- Previous search, Utilities, weight 4.
- Oldest of the three, Utilities, weight 3.
- Utilities total 7, so it is first (+4). Recreation total 5, so it is second (+2). Roads is not in these three searches and gets no category bonus.

Youth sports day is Recreation, priority 3, and an event, so its score from this search is 2 + 2 + 1 = 5, before any view points. Under the old rule a second Utilities search would have kept Utilities as the only favourite, and Recreation would have scored only while it happened to be the single last search.
