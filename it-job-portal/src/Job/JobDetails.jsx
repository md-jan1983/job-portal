// src/JobDetails.jsx
import React from "react";

const JobDetails = ({ job }) => (
  <div className="space-y-6">
    {/* <div>
      <h3 className="text-2xl font-semibold text-gray-800">Job Summary</h3>
      {job.description.map((paragraph, index) => (
        <p key={index} className="text-gray-700 mt-2">
          {paragraph}
        </p>
      ))}
    </div> */}
    <div>
      <h3 className="text-2xl font-semibold text-gray-800">Responsibilities</h3>
      <ul className="list-disc list-inside text-gray-700 mt-2">
        {job.responsibilities.map((item, index) => (
          <li key={index}>{item}</li>
        ))}
      </ul>
    </div>
    <div>
      <h3 className="text-2xl font-semibold text-gray-800">Requirements</h3>
      <ul className="list-disc list-inside text-gray-700 mt-2">
        {job.requirements.map((item, index) => (
          <li key={index}>{item}</li>
        ))}
      </ul>
    </div>
    <div>
      <h3 className="text-2xl font-semibold text-gray-800">Benefits</h3>
      <ul className="list-disc list-inside text-gray-700 mt-2">
        {/* {job.benefits.map((item, index) => (
          <li key={index}>{item}</li>
        ))} */}
      </ul>
    </div>
  </div>
);

export default JobDetails;
