import { useState, useEffect } from 'react'
import convertTimeToText from './TimeToTextHanddle';

function Clock(props) {
    const [date, setDate] = useState(new Date())
    const [title, setTitle] = useState('The Time of Your Life');
    const [tempTitle, setTempTitle] = useState(title);
   //const [isCheckedTimeText] = useState(true);
    const [showTextTime, setShowTextTime] = useState(false);

    function refreshClock() {
        fetchServerTime()
    }

    useEffect(() => {
        const timerId = setInterval(refreshClock, 1000)
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
            </div>
        </div>
  );
}

export default Clock
