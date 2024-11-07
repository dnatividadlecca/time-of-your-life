import { useState, useEffect } from 'react'
import convertTimeToText from './TimeToTextHanddle';
import moment from 'moment-timezone';

function Clock(props) {
    const [date, setDate] = useState(new Date())
    const [title, setTitle] = useState('The Time of Your Life');
    const [tempTitle, setTempTitle] = useState(title);
    const [showTextTime, setShowTextTime] = useState(false);

    const [clocks, setClocks] = useState([]);
    const timeZonesList = moment.tz.names().filter(zone => !zone.includes("GMT")); //Remove GMT times to avoid confusion
    const [timeZone, setTimeZone] = useState(timeZonesList[0]); //List first timezone from timeZonesList

    const addTimeZone = () => {
        setClocks([...clocks, timeZone]); //Adds a new timeZone
        //saveTimeZone();
    };

    const handleTimeZoneChange = (e) => {
        setTimeZone(e.target.value);
    };

    const getCurrentTime = (zone) => {
        return moment().tz(zone).format('hh:mm:ss A');
    };

    function refreshClock() {
        fetchServerTime()
    }

    useEffect(() => {
        const timerId = setInterval(refreshClock, 1000)
        fetchTimeZones();
        return function cleanup() {
            clearInterval(timerId)
        }
    }, [])

    const fetchServerTime = async () => {
        try {
            const response = await fetch('clock/serverTime');
            if (!response.ok) throw new Error('Failed to fetch server time');
            const data = await response.json();
            setDate(new Date(data.currentTime));
            console.log(data.currentTime)
        } catch (err) {
            setDate(new Date());
            console.error('Esto es un error: ' + err);
        }
    };

    const fetchTimeZones = async () => {
        try {
            const response = await fetch('clock/timezones');
            if (response.ok) {
                const data = await response.json();
                const zones = data.map(item => item.zone);
                setClocks(zones);
                console.log(data)
            }
        } catch (error) {
            setDate(new Date());
            console.error('Error fetching time zones:', error);
            alert('Error connecting to the server.');
        }
    };

    const saveTimeZone = async () => {
        const timeZoneData = {
            Zone: timeZone
        };
        console.log(timeZoneData)
        try {
            const response = await fetch('clock/timezones', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(timeZoneData)
            });

            if (response.ok) {
                alert('Time zone saved successfully!');
                fetchTimeZones();
            } else {
                alert('Failed to save time zone.');
            }
        } catch (error) {
            console.error('Error saving time zone:', error);
            alert('Error connecting to the server.');
        }
    };

    let displayText = date.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', second: '2-digit' });
    if (props.clockProps.blinkColons & (date.getSeconds() % 2 === 0) && showTextTime === false) {
        displayText = displayText.replace(/:/g, ' ')
    }
    
    const displayTime = showTextTime ? convertTimeToText(displayText) : displayText;

    let displayStyle = {
        fontFamily: props.clockProps.fontFamily,
        color: props.clockProps.fontColor,
    }

    let titleStyle = {
        fontSize: `${props.clockProps.titleFontSize}pt`,
        color: props.clockProps.titlefontColor
    }

    let clockStyle = {
        fontSize: `${props.clockProps.clockFontSize}pt`,
        color: props.clockProps.clockfontColor
    }

    const handleTitleChange = (event) => {
        setTempTitle(event.target.value);
    };

    const handleKeyDown = (event) => {
        if (event.key === 'Enter') {
            if (tempTitle.length < 1) {
                alert("Title can't be empty.");
            } else {
                setTitle(tempTitle); // Save the title
                event.target.blur();
            }
        }
    };

    return (
        <div id="Clock">
            <div id="Digits" style={displayStyle}>
                <div id="title" style={titleStyle}>
                    {title}
                </div>
                <div id="time" style={clockStyle}>
                    {displayTime}
                </div>
                <div>
                    <input
                        type="text"
                        value={tempTitle}
                        onChange={handleTitleChange}
                        onKeyDown={handleKeyDown}
                        placeholder="Customize title"
                        style={{ padding: '8px', fontSize: '1em' }}
                    />
                </div>
                <div>
                    <label>
                        <input
                            type="checkbox"
                            checked={showTextTime}
                            onChange={() => setShowTextTime(!showTextTime)}
                        />
                      Show time in text format!
                  </label>
                </div>
                <br></br>
                <div>
                    <label>Add time in: </label>
                    <select value={timeZone} onChange={handleTimeZoneChange}>
                        {timeZonesList.map((zone) => (
                            <option key={zone} value={zone}>
                                {zone}
                            </option>
                        ))}
                    </select>
                    {/*<button onClick={addTimeZone}>Add</button>*/}
                    <button onClick={saveTimeZone}>Add</button>
                </div>
            </div>
            <div style={{ marginTop: '20px' }}>
                {clocks.map((zone, index) => (
                    <div key={index}>
                        Time in {zone} : {getCurrentTime(zone)}
                    </div>
                ))}
            </div>
        </div>
  );
}

export default Clock
