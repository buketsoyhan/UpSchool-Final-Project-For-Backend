import React, { useEffect, useState } from 'react';

interface SeleniumLogDto {
  Message: string;
  SentOn: Date;
}

const LiveLogs: React.FC = () => {
  const [logs, setLogs] = useState<SeleniumLogDto[]>([]);

  useEffect(() => {
    const hubConnection = new WebSocket('wss://localhost:7008/Hubs/SeleniumLogHub');

    hubConnection.onopen = () => {
      console.log('WebSocket connected');
    };

    hubConnection.onmessage = (event) => {
      const data = JSON.parse(event.data);
      setLogs((prevLogs) => [...prevLogs, data]);
    };

    return () => {
      hubConnection.close();
    };
  }, []);

  return (
    <div>
      <div className="fakeMenu">
        <div className="fakeButtons fakeClose"></div>
        <div className="fakeButtons fakeMinimize"></div>
        <div className="fakeButtons fakeZoom"></div>
      </div>
      <div className="fakeScreen">
        {logs.map((log, index) => (
          <p key={index} className="line1">
            {log.Message} | {new Date(log.SentOn).toLocaleString()}
          </p>
        ))}
      </div>
    </div>
  );
};

export default LiveLogs;