#if METRIKA_COUNTER_ID
//АНАЛИТИКА ДОСТУПНА.

window.yandexMetricaCounterId = parseInt({{{ METRIKA_COUNTER_ID }}});


(function(m,e,t,r,i,k,a){m[i]=m[i]||function(){(m[i].a=m[i].a||[]).push(arguments)};
m[i].l=1*new Date();
for (var j = 0; j < document.scripts.length; j++) {if (document.scripts[j].src === r) { return; }}
k=e.createElement(t),a=e.getElementsByTagName(t)[0],k.async=1,k.src=r,a.parentNode.insertBefore(k,a)})
(window, document, "script", "https://mc.yandex.ru/metrika/tag.js", "ym");


ym(window.yandexMetricaCounterId, "init", {
    clickmap:true,
    trackLinks:true,
    accurateTrackBounce:true
});
	
	
function PushMetrikEvent(eventID)
{
	ym(window.yandexMetricaCounterId,'reachGoal', eventID);
	console.log(`Mirra Metrika - отправлено событие: ${eventID}`);
}


function PushMetrikEventObject(eventID, eventObject)
{
	ym(window.yandexMetricaCounterId,'reachGoal', eventID, JSON.parse(eventObject));
	console.log(`Mirra Metrika - отправлено событие с включением объекта: ${eventID}\n ${eventObject}`);
}

#else
//АНАЛИТИКА НЕДОСТУПНА! ВЫ НЕ УКАЗАЛИ ИДЕНТИФИКАТОР СЧЕТЧИКА.
#endif
