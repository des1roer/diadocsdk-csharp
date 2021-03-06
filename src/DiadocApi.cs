﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using Diadoc.Api.Annotations;
using Diadoc.Api.Cryptography;
using Diadoc.Api.Http;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Docflow;
using Diadoc.Api.Proto.Documents;
using Diadoc.Api.Proto.Events;
using Diadoc.Api.Proto.Forwarding;
using Diadoc.Api.Proto.Invoicing;
using Diadoc.Api.Proto.Recognition;

namespace Diadoc.Api
{
	public class DiadocApi : IDiadocApi
	{
		private readonly DiadocHttpApi diadocHttpApi;

		public DiadocApi(string apiClientId, string diadocApiBaseUrl, ICrypt crypt)
			: this(new DiadocHttpApi(apiClientId, new HttpClient(diadocApiBaseUrl), crypt))
		{
		}

		public DiadocApi([NotNull] DiadocHttpApi diadocHttpApi)
		{
			if (diadocHttpApi == null) throw new ArgumentNullException("diadocHttpApi");
			this.diadocHttpApi = diadocHttpApi;
		}

		/// <summary>
		///   The default value is true
		/// </summary>
		public bool UsingSystemProxy
		{
			get { return diadocHttpApi.HttpClient.UseSystemProxy; }
		}

		public void SetProxyUri([CanBeNull] string uri)
		{
			diadocHttpApi.HttpClient.SetProxyUri(uri);
		}

		public void EnableSystemProxyUsage()
		{
			diadocHttpApi.HttpClient.UseSystemProxy = true;
		}

		public void DisableSystemProxyUsage()
		{
			diadocHttpApi.HttpClient.UseSystemProxy = false;
		}

		public void SetProxyCredentials([CanBeNull] NetworkCredential proxyCredentials)
		{
			diadocHttpApi.HttpClient.SetProxyCredentials(proxyCredentials);
		}

		public void SetProxyCredentials([NotNull] string user, [NotNull] string password)
		{
			diadocHttpApi.HttpClient.SetProxyCredentials(user, password);
		}

		public void SetProxyCredentials([NotNull] string user, [NotNull] SecureString password)
		{
			diadocHttpApi.HttpClient.SetProxyCredentials(user, password);
		}

		public string Authenticate(string login, string password)
		{
			return diadocHttpApi.Authenticate(login, password);
		}

		public string AuthenticateByKey([NotNull] string key, [NotNull] string id)
		{
			if (key == null) throw new ArgumentNullException("key");
			if (id == null) throw new ArgumentNullException("id");
			return diadocHttpApi.AuthenticateByKey(key, id);
		}

		public string Authenticate(byte[] certificateBytes, bool useLocalSystemStorage = false)
		{
			if (certificateBytes == null) throw new ArgumentNullException("certificateBytes");
			return diadocHttpApi.Authenticate(certificateBytes, useLocalSystemStorage);
		}

		public string Authenticate(string thumbprint, bool useLocalSystemStorage = false)
		{
			if (thumbprint == null) throw new ArgumentNullException("thumbprint");
			return diadocHttpApi.Authenticate(thumbprint, useLocalSystemStorage);
		}

		public OrganizationUserPermissions GetMyPermissions(string authToken, string orgId)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (orgId == null) throw new ArgumentNullException("orgId");
			return diadocHttpApi.GetMyPermissions(authToken, orgId);
		}

		public OrganizationList GetMyOrganizations(string authToken, bool autoRegister = true)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			return diadocHttpApi.GetMyOrganizations(authToken, autoRegister);
		}

		public User GetMyUser(string authToken)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			return diadocHttpApi.GetMyUser(authToken);
		}

		public OrganizationList GetOrganizationsByInnKpp(string inn, string kpp, bool includeRelations = false)
		{
			if (inn == null) throw new ArgumentNullException("inn");
			return diadocHttpApi.GetOrganizationsByInnKpp(inn, kpp, includeRelations);
		}

		public Organization GetOrganizationById(string orgId)
		{
			if (orgId == null) throw new ArgumentNullException("orgId");
			return diadocHttpApi.GetOrganizationById(orgId);
		}

		public Organization GetOrganizationByBoxId(string boxId)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetOrganizationByBoxId(boxId);
		}

		public Organization GetOrganizationByFnsParticipantId(string fnsParticipantId)
		{
			if (fnsParticipantId == null) throw new ArgumentException("fnsParticipantId");
			return diadocHttpApi.GetOrganizationByFnsParticipantId(fnsParticipantId);
		}

		public Organization GetOrganizationByInnKpp(string inn, string kpp)
		{
			if (inn == null) throw new ArgumentException("inn");
			return diadocHttpApi.GetOrganizationByInnKpp(inn, kpp);
		}

		public Box GetBox(string boxId)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetBox(boxId);
		}

		public Department GetDepartment(string authToken, string orgId, string departmentId)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (orgId == null) throw new ArgumentNullException("orgId");
			if (departmentId == null) throw new ArgumentNullException("departmentId");
			return diadocHttpApi.GetDepartment(authToken, orgId, departmentId);
		}

		public void UpdateOrganizationProperties(string authToken, OrganizationPropertiesToUpdate orgProps)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (orgProps == null) throw new ArgumentNullException("orgProps");
			diadocHttpApi.UpdateOrganizationProperties(authToken, orgProps);
		}

		public BoxEventList GetNewEvents(string authToken, string boxId, string afterEventId = null)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetNewEvents(authToken, boxId, afterEventId);
		}

		public BoxEvent GetEvent(string authToken, string boxId, string eventId)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (eventId == null) throw new ArgumentNullException("eventId");
			return diadocHttpApi.GetEvent(authToken, boxId, eventId);
		}

		public Message PostMessage(string authToken, MessageToPost msg, string operationId = null)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (msg == null) throw new ArgumentNullException("msg");
			return diadocHttpApi.PostMessage(authToken, msg, operationId);
		}

		public MessagePatch PostMessagePatch(string authToken, MessagePatchToPost patch, string operationId = null)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (patch == null) throw new ArgumentNullException("patch");
			return diadocHttpApi.PostMessagePatch(authToken, patch, operationId);
		}

		public void PostRoamingNotification(string authToken, RoamingNotificationToPost notification)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (notification == null) throw new ArgumentNullException("notification");
			diadocHttpApi.PostRoamingNotification(authToken, notification);
		}

		public void Delete(string authToken, string boxId, string messageId, string documentId)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			diadocHttpApi.Delete(authToken, boxId, messageId, documentId);
		}

		public void Restore(string authToken, string boxId, string messageId, string documentId)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			diadocHttpApi.Restore(authToken, boxId, messageId, documentId);
		}

		public void MoveDocuments(string authToken, DocumentsMoveOperation query)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (query == null) throw new ArgumentNullException("query");
			diadocHttpApi.MoveDocuments(authToken, query);
		}

		public byte[] GetEntityContent(string authToken, string boxId, string messageId, string entityId)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (entityId == null) throw new ArgumentNullException("entityId");
			return diadocHttpApi.GetEntityContent(authToken, boxId, messageId, entityId);
		}

		public GeneratedFile GenerateDocumentReceiptXml(string authToken, string boxId, string messageId, string attachmentId,
			Signer signer)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (attachmentId == null) throw new ArgumentNullException("attachmentId");
			if (signer == null) throw new ArgumentNullException("signer");
			return diadocHttpApi.GenerateDocumentReceiptXml(authToken, boxId, messageId, attachmentId, signer);
		}

		public GeneratedFile GenerateInvoiceDocumentReceiptXml(string authToken, string boxId, string messageId,
			string attachmentId, Signer signer)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (attachmentId == null) throw new ArgumentNullException("attachmentId");
			if (signer == null) throw new ArgumentNullException("signer");
			return diadocHttpApi.GenerateInvoiceDocumentReceiptXml(authToken, boxId, messageId, attachmentId, signer);
		}

		public GeneratedFile GenerateInvoiceCorrectionRequestXml(string authToken, string boxId, string messageId,
			string attachmentId, InvoiceCorrectionRequestInfo correctionInfo)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (attachmentId == null) throw new ArgumentNullException("attachmentId");
			return diadocHttpApi.GenerateInvoiceCorrectionRequestXml(authToken, boxId, messageId, attachmentId, correctionInfo);
		}

		public GeneratedFile GenerateRevocationRequestXml(string authToken, string boxId, string messageId,
			string attachmentId, RevocationRequestInfo revocationRequestInfo)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (attachmentId == null) throw new ArgumentNullException("attachmentId");
			return diadocHttpApi.GenerateRevocationRequestXml(authToken, boxId, messageId, attachmentId, revocationRequestInfo);
		}

		public GeneratedFile GenerateSignatureRejectionXml(string authToken, string boxId, string messageId,
			string attachmentId, SignatureRejectionInfo signatureRejectionInfo)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (attachmentId == null) throw new ArgumentNullException("attachmentId");
			return diadocHttpApi.GenerateSignatureRejectionXml(authToken, boxId, messageId, attachmentId, signatureRejectionInfo);
		}

		public InvoiceCorrectionRequestInfo GetInvoiceCorrectionRequestInfo(string authToken, string boxId, string messageId,
			string entityId)
		{
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (entityId == null) throw new ArgumentNullException("entityId");
			return diadocHttpApi.GetInvoiceCorrectionRequestInfo(authToken, boxId, messageId, entityId);
		}

		public GeneratedFile GenerateInvoiceXml(string authToken, InvoiceInfo invoiceInfo, bool disableValidation = false)
		{
			if (invoiceInfo == null) throw new ArgumentNullException("invoiceInfo");
			return diadocHttpApi.GenerateInvoiceXml(authToken, invoiceInfo, disableValidation);
		}

		public GeneratedFile GenerateInvoiceRevisionXml(string authToken, InvoiceInfo invoiceRevisionInfo,
			bool disableValidation = false)
		{
			if (invoiceRevisionInfo == null) throw new ArgumentNullException("invoiceRevisionInfo");
			return diadocHttpApi.GenerateInvoiceRevisionXml(authToken, invoiceRevisionInfo, disableValidation);
		}

		public GeneratedFile GenerateInvoiceCorrectionXml(string authToken, InvoiceCorrectionInfo invoiceCorrectionInfo,
			bool disableValidation = false)
		{
			if (invoiceCorrectionInfo == null) throw new ArgumentNullException("invoiceCorrectionInfo");
			return diadocHttpApi.GenerateInvoiceCorrectionXml(authToken, invoiceCorrectionInfo, disableValidation);
		}

		public GeneratedFile GenerateInvoiceCorrectionRevisionXml(string authToken,
			InvoiceCorrectionInfo invoiceCorrectionRevision, bool disableValidation = false)
		{
			if (invoiceCorrectionRevision == null) throw new ArgumentNullException("invoiceCorrectionRevision");
			return diadocHttpApi.GenerateInvoiceCorrectionRevisionXml(authToken, invoiceCorrectionRevision, disableValidation);
		}

		public GeneratedFile GenerateTorg12XmlForSeller(string authToken, Torg12SellerTitleInfo sellerInfo,
			bool disableValidation = false)
		{
			if (sellerInfo == null) throw new ArgumentNullException("sellerInfo");
			return diadocHttpApi.GenerateTorg12XmlForSeller(authToken, sellerInfo, disableValidation);
		}

		public GeneratedFile GenerateTorg12XmlForBuyer(string authToken, Torg12BuyerTitleInfo buyerInfo, string boxId,
			string sellerTitleMessageId, string sellerTitleAttachmentId)
		{
			if (buyerInfo == null) throw new ArgumentNullException("buyerInfo");
			return diadocHttpApi.GenerateTorg12XmlForBuyer(authToken, buyerInfo, boxId, sellerTitleMessageId,
				sellerTitleAttachmentId);
		}

		public GeneratedFile GenerateAcceptanceCertificateXmlForSeller(string authToken,
			AcceptanceCertificateSellerTitleInfo sellerInfo, bool disableValidation = false)
		{
			if (sellerInfo == null) throw new ArgumentNullException("sellerInfo");
			return diadocHttpApi.GenerateAcceptanceCertificateXmlForSeller(authToken, sellerInfo, disableValidation);
		}

		public GeneratedFile GenerateAcceptanceCertificateXmlForBuyer(string authToken,
			AcceptanceCertificateBuyerTitleInfo buyerInfo, string boxId, string sellerTitleMessageId,
			string sellerTitleAttachmentId)
		{
			if (buyerInfo == null) throw new ArgumentNullException("buyerInfo");
			return diadocHttpApi.GenerateAcceptanceCertificateXmlForBuyer(authToken, buyerInfo, boxId, sellerTitleMessageId,
				sellerTitleAttachmentId);
		}

		public GeneratedFile GenerateUniversalTransferDocumentXmlForSeller(string authToken,
			UniversalTransferDocumentSellerTitleInfo sellerInfo, bool disableValidation = false)
		{
			if (sellerInfo == null) throw new ArgumentNullException("sellerInfo");
			return diadocHttpApi.GenerateUniversalTransferDocumentXmlForSeller(authToken, sellerInfo, disableValidation);
		}

		public Message GetMessage(string authToken, string boxId, string messageId, bool withOriginalSignature = false)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			return diadocHttpApi.GetMessage(authToken, boxId, messageId, withOriginalSignature);
		}

		public Message GetMessage(string authToken, string boxId, string messageId, string entityId, bool withOriginalSignature = false)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (entityId == null) throw new ArgumentNullException("entityId");
			return diadocHttpApi.GetMessage(authToken, boxId, messageId, entityId, withOriginalSignature);
		}

		public void RecycleDraft(string authToken, string boxId, string draftId)
		{
			diadocHttpApi.RecycleDraft(authToken, boxId, draftId);
		}

		public Message SendDraft(string authToken, DraftToSend draftToSend, string operationId = null)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (draftToSend == null) throw new ArgumentNullException("draftToSend");
			return diadocHttpApi.SendDraft(authToken, draftToSend, operationId);
		}

		public PrintFormResult GeneratePrintForm(string authToken, string boxId, string messageId, string documentId)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			if (string.IsNullOrEmpty(messageId)) throw new ArgumentNullException("messageId");
			if (string.IsNullOrEmpty(documentId)) throw new ArgumentNullException("documentId");
			return diadocHttpApi.GeneratePrintForm(authToken, boxId, messageId, documentId);
		}

		public string GeneratePrintFormFromAttachment(string authToken, DocumentType documentType, byte[] content,
			string fromBoxId = null)
		{
			return diadocHttpApi.GeneratePrintFormFromAttachment(authToken, documentType, content, fromBoxId);
		}

		public PrintFormResult GetGeneratedPrintForm(string authToken, DocumentType documentType, string printFormId)
		{
			if (string.IsNullOrEmpty(printFormId)) throw new ArgumentNullException("printFormId");
			return diadocHttpApi.GetGeneratedPrintForm(authToken, documentType, printFormId);
		}

		public string Recognize(string fileName, byte[] content)
		{
			if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName");
			return diadocHttpApi.Recognize(fileName, content);
		}

		public Recognized GetRecognized(string recognitionId)
		{
			if (string.IsNullOrEmpty(recognitionId)) throw new ArgumentNullException("recognitionId");
			return diadocHttpApi.GetRecognized(recognitionId);
		}

		public DocumentList GetDocuments(string authToken, string boxId, string filterCategory, string counteragentBoxId,
			DateTime? timestampFrom, DateTime? timestampTo, string fromDocumentDate, string toDocumentDate, string departmentId,
			bool excludeSubdepartments, string afterIndexKey)
		{
			return diadocHttpApi.GetDocuments(authToken, boxId, filterCategory, counteragentBoxId, timestampFrom, timestampTo,
				fromDocumentDate, toDocumentDate, departmentId, excludeSubdepartments, afterIndexKey);
		}

		public DocumentList GetDocuments(string authToken, DocumentsFilter filter)
		{
			return diadocHttpApi.GetDocuments(authToken, filter);
		}

		public Document GetDocument(string authToken, string boxId, string messageId, string entityId)
		{
			return diadocHttpApi.GetDocument(authToken, boxId, messageId, entityId);
		}

		public GetDocflowBatchResponse GetDocflows(string authToken, string boxId, GetDocflowBatchRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetDocflows(authToken, boxId, request);
		}

		public GetDocflowEventsResponse GetDocflowEvents(string authToken, string boxId, GetDocflowEventsRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetDocflowEvents(authToken, boxId, request);
		}

		public SearchDocflowsResponse SearchDocflows(string authToken, string boxId, SearchDocflowsRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.SearchDocflows(authToken, boxId, request);
		}

		public GetDocflowsByPacketIdResponse GetDocflowsByPacketId(string authToken, string boxId,
			GetDocflowsByPacketIdRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetDocflowsByPacketId(authToken, boxId, request);
		}

		public ForwardDocumentResponse ForwardDocument(string authToken, string boxId, ForwardDocumentRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.ForwardDocument(authToken, boxId, request);
		}

		public GetForwardedDocumentsResponse GetForwardedDocuments(string authToken, string boxId,
			GetForwardedDocumentsRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetForwardedDocuments(authToken, boxId, request);
		}

		public GetForwardedDocumentEventsResponse GetForwardedDocumentEvents(string authToken, string boxId,
			GetForwardedDocumentEventsRequest request)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetForwardedDocumentEvents(authToken, boxId, request);
		}

		public byte[] GetForwardedEntityContent(string authToken, string boxId, ForwardedDocumentId forwardedDocumentId,
			string entityId)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GetForwardedEntityContent(authToken, boxId, forwardedDocumentId, entityId);
		}

		public IDocumentProtocolResult GenerateForwardedDocumentProtocol(string authToken, string boxId,
			ForwardedDocumentId forwardedDocumentId)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			return diadocHttpApi.GenerateForwardedDocumentProtocol(authToken, boxId, forwardedDocumentId);
		}

		public bool CanSendInvoice(string authToken, string boxId, byte[] certificateBytes)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			if (certificateBytes == null || certificateBytes.Length == 0) throw new ArgumentNullException("certificateBytes");
			return diadocHttpApi.CanSendInvoice(authToken, boxId, certificateBytes);
		}

		public void SendFnsRegistrationMessage(string authToken, string boxId,
			FnsRegistrationMessageInfo fnsRegistrationMessageInfo)
		{
			if (string.IsNullOrEmpty(boxId)) throw new ArgumentNullException("boxId");
			if (!fnsRegistrationMessageInfo.Certificates.Any()) throw new ArgumentException("fnsRegistrationMessageInfo");
			diadocHttpApi.SendFnsRegistrationMessage(authToken, boxId, fnsRegistrationMessageInfo);
		}

		public Counteragent GetCounteragent(string authToken, string myOrgId, string counteragentOrgId)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(myOrgId)) throw new ArgumentNullException("myOrgId");
			if (string.IsNullOrEmpty(counteragentOrgId)) throw new ArgumentNullException("counteragentOrgId");
			return diadocHttpApi.GetCounteragent(authToken, myOrgId, counteragentOrgId);
		}

		public CounteragentList GetCounteragents(string authToken, string myOrgId, string counteragentStatus,
			string afterIndexKey)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(myOrgId)) throw new ArgumentNullException("myOrgId");
			return diadocHttpApi.GetCounteragents(authToken, myOrgId, counteragentStatus, afterIndexKey);
		}

		public CounteragentCertificateList GetCounteragentCertificates(string authToken, string myOrgId,
			string counteragentOrgId)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(myOrgId)) throw new ArgumentNullException("myOrgId");
			if (string.IsNullOrEmpty(counteragentOrgId)) throw new ArgumentNullException("counteragentOrgId");
			return diadocHttpApi.GetCounteragentCertificates(authToken, myOrgId, counteragentOrgId);
		}

		public void BreakWithCounteragent(string authToken, string myOrgId, string counteragentOrgId, string comment)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(myOrgId)) throw new ArgumentNullException("myOrgId");
			if (string.IsNullOrEmpty(counteragentOrgId)) throw new ArgumentNullException("counteragentOrgId");
			diadocHttpApi.BreakWithCounteragent(authToken, myOrgId, counteragentOrgId, comment);
		}

		public string UploadFileToShelf(string authToken, byte[] data)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException("authToken");
			if (data == null) throw new ArgumentNullException("data");
			return diadocHttpApi.UploadFileToShelf(authToken, data);
		}

		public byte[] GetFileFromShelf(string authToken, string nameOnShelf)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(nameOnShelf)) throw new ArgumentNullException("nameOnShelf");
			return diadocHttpApi.GetFileFromShelf(authToken, nameOnShelf);
		}

		public RussianAddress ParseRussianAddress(string address)
		{
			return diadocHttpApi.ParseRussianAddress(address);
		}

		public InvoiceInfo ParseInvoiceXml(byte[] invoiceXmlContent)
		{
			return diadocHttpApi.ParseInvoiceXml(invoiceXmlContent);
		}

		public Torg12SellerTitleInfo ParseTorg12SellerTitleXml(byte[] xmlContent)
		{
			return diadocHttpApi.ParseTorg12SellerTitleXml(xmlContent);
		}

		public AcceptanceCertificateSellerTitleInfo ParseAcceptanceCertificateSellerTitleXml(byte[] xmlContent)
		{
			return diadocHttpApi.ParseAcceptanceCertificateSellerTitleXml(xmlContent);
		}

		public UniversalTransferDocumentSellerTitleInfo ParseUniversalTransferDocumentSellerTitleXml(byte[] xmlContent)
		{
			return diadocHttpApi.ParseUniversalTransferDocumentSellerTitleXml(xmlContent);
		}

		public OrganizationUsersList GetOrganizationUsers(string authToken, string orgId)
		{
			if (string.IsNullOrEmpty(authToken)) throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(orgId)) throw new ArgumentNullException("orgId");
			return diadocHttpApi.GetOrganizationUsers(authToken, orgId);
		}

		public List<Organization> GetOrganizationsByInnList(GetOrganizationsByInnListRequest innList)
		{
			if (innList == null)
				throw new ArgumentNullException("innList");
			return diadocHttpApi.GetOrganizationsByInnList(innList);
		}

		public List<OrganizationWithCounteragentStatus> GetOrganizationsByInnList(string authToken, string myOrgId,
			GetOrganizationsByInnListRequest innList)
		{
			if (string.IsNullOrEmpty(authToken))
				throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(myOrgId))
				throw new ArgumentNullException("myOrgId");
			if (innList == null)
				throw new ArgumentNullException("innList");
			return diadocHttpApi.GetOrganizationsByInnList(authToken, myOrgId, innList);
		}

		public RevocationRequestInfo ParseRevocationRequestXml(byte[] revocationRequestXmlContent)
		{
			return diadocHttpApi.ParseRevocationRequestXml(revocationRequestXmlContent);
		}

		public SignatureRejectionInfo ParseSignatureRejectionXml(byte[] signatureRejectionXmlContent)
		{
			return diadocHttpApi.ParseSignatureRejectionXml(signatureRejectionXmlContent);
		}

		public IDocumentProtocolResult GenerateDocumentProtocol(string authToken, string boxId, string messageId,
			string documentId)
		{
			return diadocHttpApi.GenerateDocumentProtocol(authToken, boxId, messageId, documentId);
		}

		public IDocumentZipGenerationResult GenerateDocumentZip(string authToken, string boxId, string messageId,
			string documentId, bool fullDocflow)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (messageId == null) throw new ArgumentNullException("messageId");
			if (documentId == null) throw new ArgumentNullException("documentId");
			return diadocHttpApi.GenerateDocumentZip(authToken, boxId, messageId, documentId, fullDocflow);
		}

		public DocumentList GetDocumentsByCustomId(string authToken, string boxId, string customDocumentId)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			if (boxId == null) throw new ArgumentNullException("boxId");
			if (customDocumentId == null) throw new ArgumentNullException("customDocumentId");
			return diadocHttpApi.GetDocumentsByCustomId(authToken, boxId, customDocumentId);
		}

		public PrepareDocumentsToSignResponse PrepareDocumentsToSign(string authToken, PrepareDocumentsToSignRequest request,
			bool excludeContent = false)
		{
			if (authToken == null) throw new ArgumentNullException("authToken");
			return diadocHttpApi.PrepareDocumentsToSign(authToken, request, excludeContent);
		}

		public AsyncMethodResult CloudSign(string authToken, CloudSignRequest request, string certificateThumbprint)
		{
			if (request == null) throw new ArgumentNullException("request");
			return diadocHttpApi.CloudSign(authToken, request, certificateThumbprint);
		}

		public CloudSignResult WaitCloudSignResult(string authToken, string taskId, TimeSpan? timeout = null)
		{
			if (string.IsNullOrEmpty(taskId)) throw new ArgumentNullException("taskId");
			return diadocHttpApi.WaitCloudSignResult(authToken, taskId, timeout);
		}

		public AsyncMethodResult CloudSignConfirm(string authToken, string cloudSignToken, string confirmationCode,
			ContentLocationPreference? locationPreference = null)
		{
			if (string.IsNullOrEmpty(cloudSignToken)) throw new ArgumentNullException("cloudSignToken");
			if (string.IsNullOrEmpty(confirmationCode)) throw new ArgumentNullException("confirmationCode");
			return diadocHttpApi.CloudSignConfirm(authToken, cloudSignToken, confirmationCode, locationPreference);
		}

		public CloudSignConfirmResult WaitCloudSignConfirmResult(string authToken, string taskId, TimeSpan? timeout = null)
		{
			if (string.IsNullOrEmpty(taskId)) throw new ArgumentNullException("taskId");
			return diadocHttpApi.WaitCloudSignConfirmResult(authToken, taskId, timeout);
		}

		public AsyncMethodResult AcquireCounteragent(string authToken, string myOrgId, AcquireCounteragentRequest request,
			string myDepartmentId = null)
		{
			if (request == null) throw new ArgumentNullException("request");
			return diadocHttpApi.AcquireCounteragent(authToken, myOrgId, request, myDepartmentId);
		}

		public AcquireCounteragentResult WaitAcquireCounteragentResult(string authToken, string taskId,
			TimeSpan? timeout = null)
		{
			if (string.IsNullOrEmpty(taskId)) throw new ArgumentNullException("taskId");
			return diadocHttpApi.WaitAcquireCounteragentResult(authToken, taskId, timeout);
		}

		public DocumentList GetDocumentsByMessageId(string authToken, string boxId, string messageId)
		{
			if (string.IsNullOrEmpty(authToken))
				throw new ArgumentNullException("authToken");
			if (string.IsNullOrEmpty(boxId))
				throw new ArgumentNullException("boxId");
			if (string.IsNullOrEmpty(messageId))
				throw new ArgumentNullException("messageId");
			return diadocHttpApi.GetDocumentsByMessageId(authToken, boxId, messageId);
		}
	}
}