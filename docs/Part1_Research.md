# Part 1, Task 1 — Research: User Engagement Strategies for a Municipal Services Application

Module: PROG7312 / AAPD7112 — Advanced Application Development
Student name: `<your name>`
Student number: `<your student number>`
Referencing style: IEEE

---

## Five user engagement strategies considered

1. **Progressive completion (guided reporting) — the chosen strategy.** The application shows the resident how far the report has progressed and what step remains, using a progress bar and a short changing message. Visible advancement towards a goal supports persistence in completing a task [4], and showing where a user is inside a process is recommended guidance for making content usable for a wide range of people [5].

2. **Closed-loop feedback on submitted reports.** The municipality acknowledges each report, gives it a reference number, and reports back on progress. This matches the citizen-centric emphasis on consultation, transparency, and accountability observed in South African municipal e-participation work [3].

3. **Clear language and cognitive support for a diverse user base.** Plain labels, short instructions, and unambiguous error messages reduce the skill barrier that South African local e-participation frameworks list as a determining factor for adoption [2], and they follow published guidance on clear, understandable content [5].

4. **Stakeholder participation and co-production of service reporting.** Residents are treated as contributors rather than recipients. Research on basic service technology demonstrations in distressed South African municipalities concludes that active participation by all stakeholders, adequate resources, and cohesive networks are necessary conditions for inclusive innovation to succeed [1].

5. **Building trust and demonstrating the value of the channel.** Citizens must see the platform as valuable, trustworthy, and safe before they will use it, alongside considerations of access, skills, transparency, and funding [2]. Adaptable, malleable tools are more sustainable in municipalities with uneven capacity [3].

---

## Explanation and justification of the chosen strategy: progressive completion

Progressive completion was selected for the Municipal Services Application because it addresses the single biggest risk in citizen fault reporting: a resident who opens the reporting screen but never finishes it, or who submits a report so thin that no repair team can act on it. In the application, a progress bar and a short message under it respond to every keystroke and selection. Entering a location contributes twenty-five percent, choosing a category another twenty-five, writing a description thirty, and attaching a photograph or document the final twenty. The message changes with the state of the form, moving from "start by telling us where the problem is" to an invitation to submit once the report is complete. Nothing is hidden behind extra screens, and the resident is never told only that something is missing without being told which detail to supply next.

The justification rests on three arguments. First, the strategy is grounded in evidence about goal-directed behaviour. Nunes and Drèze demonstrate that framing a task as already underway, rather than not yet begun, increases persistence towards finishing it [4]. A bar that visibly advances as each field is completed applies that finding directly to municipal fault reporting. Second, the strategy follows published usability guidance. Guidance on making content usable recommends that users be shown where they are inside a process and what remains, in clear language, so that people who lose focus can re-orient themselves and continue [5]. In a municipality serving residents with widely differing levels of literacy and digital experience, that orientation is not a decoration but a requirement.

Third, the strategy fits the South African municipal context described in the literature. Hart, Booyens and Sinyolo find that inclusive innovation in distressed municipalities succeeds only where active participation by stakeholders is encouraged and fostered, supported by sufficient resources and cohesive networks [1]. A reporting channel that quietly guides a resident to a complete submission encourages exactly that participation. Mahwai and colleagues identify value, access, skills, transparency, trust, and funding as the concepts local municipalities must address for e-participation to become a reality [2]; a progress indicator lowers the skills barrier and makes the value of finishing immediately visible. Ayodele and colleagues report that malleable, adaptable technologies are more likely to be sustained by officials working with limited capacity [3], and a progress calculation built from four weighted fields can be re-weighted as departmental needs change.

Alternatives were rejected on practical grounds. Points and badges require accounts, moderation, and an incentive budget that a distressed municipality is unlikely to sustain. Community activity feeds require data the application does not yet hold. Progressive completion needs no additional infrastructure, works entirely inside the existing Windows Form, and produces a measurable benefit for the municipality: reports that contain a location, a category, a description, and often supporting media, which is precisely the information a repair crew needs before it can be dispatched.

---

## References

[1] T. G. B. Hart, I. Booyens, and S. Sinyolo, "Innovation for development in South Africa: experiences with basic service technologies in distressed municipalities," *Forum for Development Studies*, vol. 47, no. 1, pp. 23–47, 2020, doi: 10.1080/08039410.2019.1654543.

[2] N. Mahwai, S. Dlamini, M. Herselman, C. Phiri, and I. Meyer, "A scoping review for proposing an eParticipation framework for South African local municipalities," in *IST-Africa 2023 Conference Proceedings*, IST-Africa Institute and IIMC, 2023, doi: 10.23919/IST-Africa60249.2023.10187880.

[3] O. Ayodele, P. Plantinga, D. Sanchez-Bentacourt, S. Dlamini, and N. Dlamini, "How do researchers and public officials co-navigate e-participation implementation? An action-research experience with South African municipalities," *Systems*, vol. 13, no. 2, Art. no. 125, 2025, doi: 10.3390/systems13020125.

[4] J. C. Nunes and X. Drèze, "The endowed progress effect: How artificial advancement increases effort," *Journal of Consumer Research*, vol. 32, no. 4, pp. 504–512, Mar. 2006, doi: 10.1086/500480.

[5] W3C, "Making content usable for people with cognitive and learning disabilities," W3C Working Group Note, 29 Apr. 2021. [Online]. Available: https://www.w3.org/TR/2021/NOTE-coga-usable-20210429/. [Accessed: 2 Sep. 2026].
