export const environment = {
  production: true,
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
    },
  },
  localization: {
    defaultResourceName: 'Speedwave',
  },
};
