export const environment = {
  production: false,
  application: {
    name: 'Speedwave',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44397',
    clientId: 'Speedwave_App',
    dummyClientSecret: '1q2w3e*',
    scope: 'Speedwave',
    showDebugInformation: true,
    oidc: false,
    requireHttps: true,
  },
  apis: {
    default: {
      url: 'https://localhost:44397',
      rootNamespace: 'HDHDC.Speedwave'
    },
  },
  localization: {
    defaultResourceName: 'Speedwave',
  },
};
