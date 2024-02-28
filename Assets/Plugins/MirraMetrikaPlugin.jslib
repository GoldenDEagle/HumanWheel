mergeInto(LibraryManager.library, {

/*METRIKS*/
AddMetriksEvent: function(eventId)
{
	try
	{
		PushMetrikEvent(UTF8ToString(eventId));
	}
	catch(error)
	{
		console.log(`Метрика - ошибка отправки события: ${error}`);
	}
},

AddMetriksEventObject: function(eventId, eventObject)
{
	try
	{
		PushMetrikEventObject(UTF8ToString(eventId), UTF8ToString(eventObject));
	}
	catch(error)
	{
		console.log(`Метрика - ошибка отправки события с включением объекта: ${error}`);
	}
},
});